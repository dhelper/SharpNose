using System;
using System.Configuration;
using SharpNose.Core;
using SharpNose.SDK;

namespace SharpNose
{
    internal class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Welcome to #Nose...");
            Console.WriteLine();
            Console.WriteLine();

            int result = 0;

            ArgumentParser parser = new ArgumentParser(args);
            switch (parser.SelectedOperation)
            {
                case Operation.RunTests:
                    Console.WriteLine("Searching for valid .Net assemblies...");
                    result = RunTest(new ValidDotNetAssemblies(new ValidAssemblyDiscovery(parser)));

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
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Using #Nose is simple:");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("SharpNose [Target Directory]");
            Console.WriteLine();
            ShowOptionalParameters();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = foregroundColor;
        }

        private static void ShowOptionalParameters()
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Optional Parameters:");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(" /r, /recursive");
            Console.WriteLine("     Recursively search all directories in Target Directory for test assemblies");
            Console.WriteLine();
            Console.WriteLine(" /m:[Regex], /mask:[Regex]");
            Console.WriteLine("     Only use test assemblies whose full name matches the [Regex]");
            Console.WriteLine();
        }

        private static int RunTest(ValidDotNetAssemblies validDotNetAssemblies)
        {
            PluginConfigurations configurations = new PluginConfigurations();

            Plugins configSection = ConfigurationManager.GetSection("plugins") as Plugins;
            foreach (RunnerConfiguration runnerConfiguration in configSection.TestRunners)
            {
                TestRunnerConfiguration testRunnerConfiguration =
                    new TestRunnerConfiguration(
                        runnerConfiguration.Name, runnerConfiguration.Path, runnerConfiguration.AdditionalArguments);

                configurations.AddConfiguration(testRunnerConfiguration);
            }

            Runner runner = new Runner(configurations);

            runner.messageRecieved += OnMessageRecieved;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Starting run...");
            int res = runner.RunTests(validDotNetAssemblies);

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