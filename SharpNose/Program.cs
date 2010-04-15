using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using SharpNose.Core;
using System.Linq;

namespace SharpNose
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Welcome to #Nose...");
            Console.WriteLine();
            Console.WriteLine();

            var result = 0;

            var parser = new ArgumentParser(args);
            switch (parser.SelectedOperation)
            {
                case Operation.RunTests:
                    result = RunTest(args);

                    break;
                case Operation.Config:
                case Operation.Invalid:
                default:
                    Console.ForegroundColor = foregroundColor;

                    ShowHelp();
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Visit www.Typemock.com to learn more about unit testing and TDD");
            Console.ForegroundColor = foregroundColor;

            return result;
        }

        private static void ShowHelp()
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Using #Nose is simple:");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("SharpNose [Target Directory]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = foregroundColor;
        }

        static int RunTest(IEnumerable<string> args)
        {
            var configurations = new PluginConfigurations();

            var configSection = ConfigurationManager.GetSection("plugins") as Plugins;
            foreach (RunnerConfiguration runnerConfiguration in configSection.TestRunners)
            {
                var testRunnerConfiguration = new TestRunnerConfiguration(runnerConfiguration.Name,
                                                                          runnerConfiguration.Path,
                                                                          runnerConfiguration.AdditionalArguments);

                configurations.AddConfiguration(testRunnerConfiguration);
            }

            var runner = new Runner(configurations);

            runner.messageRecieved += OnMessageRecieved;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Starting run...");
            var res = runner.RunTests(Path.GetFullPath(args.First()));

            runner.messageRecieved -= OnMessageRecieved;

            return res;
        }

        private static void OnMessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
}