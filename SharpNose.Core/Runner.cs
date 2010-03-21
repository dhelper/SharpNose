using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpNose.Core
{
    public class Runner
    {
        [ImportMany]
        private TestDiscovery[] testDiscoveries;

        public EventHandler<MessageRecievedEventArgs> messageRecieved = (sender, args) => {};
        private readonly Dictionary<string, TestRunnerConfiguration> configurations;

        public Runner()
        {
            configurations = new Dictionary<string, TestRunnerConfiguration>();
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void RunTests(string path)
        {
            foreach (var testDiscovery in testDiscoveries)
            {
                testDiscovery.TestRunnerPath = configurations[testDiscovery.Name].Path;
                
                var result = testDiscovery.FindTestAssembliesInPath(path);

                messageRecieved(this, 
                    new MessageRecievedEventArgs(string.Format("Found {0} assemblies in path", result.Count())));

                var commandLineInfo = testDiscovery.GenerateCommandLine(result);
                
                messageRecieved(this, 
                    new MessageRecievedEventArgs(string.Format("Running: {0}", commandLineInfo)));

                string testRunner = Path.GetFullPath(commandLineInfo.TestRunner);
                var startInfo = new ProcessStartInfo(testRunner, commandLineInfo.Arguments)
                {
                    WorkingDirectory = path,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var proc = Process.Start(startInfo);
                do
                {
                    string output = proc.StandardOutput.ReadLine();
                    Console.WriteLine(output);
                } while (proc.StandardOutput.EndOfStream == false);

                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    // TODO: proper error handling
                    messageRecieved(this,
                                    new MessageRecievedEventArgs(
                                        string.Format("Process returned error code {0}",proc.ExitCode)));
                }
            }

        }

        public void AddConfiguration(TestRunnerConfiguration configuration)
        {
            configurations.Add(configuration.Name, configuration);
        }
    }
}
