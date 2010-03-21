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
                case Operation.Config:
                    //            		if(ConfigSystem() == false)
                    //            		{
                    //            			result = 1;
                    //            		}
                    break;
                case Operation.RunTests:
                    result = RunTest(args);
                    break;
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
            //            Console.WriteLine("Configure the path to NUnit using:");
            //            Console.ForegroundColor = foregroundColor;
            //            Console.WriteLine("SharpNose -config");
            //            Console.WriteLine();
            Console.ForegroundColor = foregroundColor;
        }

        static int RunTest(IEnumerable<string> args)
        {
            var runner = new Runner();

            var configSection = ConfigurationManager.GetSection("plugins") as Plugins;
            foreach (RunnerConfiguration runnerConfiguration in configSection.TestRunners)
            {
                var testRunnerConfiguration = new TestRunnerConfiguration(runnerConfiguration.Name,
                                                                          runnerConfiguration.Path,
                                                                          runnerConfiguration.AdditionalArguments);

                runner.AddConfiguration(testRunnerConfiguration);
            }

            runner.messageRecieved += OnMessageRecieved;

            runner.RunTests(Path.GetFullPath(args.First()));

            runner.messageRecieved -= OnMessageRecieved;

            return 0;
        }

        private static void OnMessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
}