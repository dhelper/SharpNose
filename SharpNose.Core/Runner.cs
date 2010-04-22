using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpNose.SDK;

namespace SharpNose.Core
{
    public class Runner
    {
        [ImportMany]
        private TestDiscovery[] testDiscoveries;

        [Export("Configurations")]
        private PluginConfigurations Configurations { get; set; }

        public EventHandler<MessageRecievedEventArgs> messageRecieved = (sender, args) => { };
        private CompositionContainer container;

        public Runner(PluginConfigurations configurations)
        {
            this.Configurations = configurations;
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pluginPath = Path.Combine(assemblyPath, "Plugins");
            var catalog = new DirectoryCatalog(pluginPath);
            container = new CompositionContainer(catalog);
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

                var proc = Process.Start(startInfo);
                do
                {
                    var output = proc.StandardOutput.ReadLine();
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
    }
}