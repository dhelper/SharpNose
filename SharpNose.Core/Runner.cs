using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpNose.Plugins;

namespace SharpNose.Core
{
    public class Runner
    {
        [Export] private PluginConfigurations configurations;

        public EventHandler<MessageRecievedEventArgs> messageRecieved = (sender, args) => { };
        
        [ImportMany] 
        private TestDiscovery[] testDiscoveries;

        public Runner(PluginConfigurations configurations)
        {
            this.configurations = configurations;
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pluginPath = Path.Combine(assemblyPath, "Plugins");
            var catalog = new DirectoryCatalog(pluginPath);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public int RunTests(string path)
        {
            int returnedValue = 0;
            foreach (TestDiscovery testDiscovery in testDiscoveries)
            {
                IEnumerable<string> result = testDiscovery.FindTestAssembliesInPath(path);

                messageRecieved(this,
                                new MessageRecievedEventArgs(string.Format("Found {0} assemblies in path",
                                                                           result.Count())));
                if (result.Count() == 0)
                {
                    continue;
                }

                CommandLineInfo commandLineInfo = testDiscovery.GenerateCommandLine(result);

                messageRecieved(this,
                                new MessageRecievedEventArgs(string.Format("Running: {0}", commandLineInfo)));

                string runnerExec = Path.GetFullPath(commandLineInfo.TestRunner);
                string arguments = commandLineInfo.Arguments + " " + commandLineInfo.AddtionalArguments;
                var startInfo = new ProcessStartInfo(runnerExec, arguments)
                                    {
                                        WorkingDirectory = path,
                                        RedirectStandardOutput = true,
                                        UseShellExecute = false
                                    };

                Process proc = Process.Start(startInfo);
                do
                {
                    string output = proc.StandardOutput.ReadLine();
                    Console.WriteLine(output);
                } while (proc.StandardOutput.EndOfStream == false);

                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    returnedValue = proc.ExitCode;
                    // TODO: proper error handling
                    messageRecieved(this,
                                    new MessageRecievedEventArgs(
                                        string.Format("Process returned error code {0}", returnedValue)));
                }
            }

            return returnedValue;
        }

        ////public void AddConfiguration(TestRunnerConfiguration configuration)
        ////{
        ////    configurations.Add(configuration.Name, configuration);
        ////}
    }
}