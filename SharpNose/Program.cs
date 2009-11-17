using System;
using System.Diagnostics;
using System.Linq;
using SharpNose.Core;
using SharpNose.Properties;

namespace SharpNose
{
    class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("DotNetNose Started...");
            Console.WriteLine();
            Console.WriteLine();

            var result  = 0;
            var parser = new ArgumentParser(args);
            switch (parser.SelectedOperation) {
            	case Operation.Config:
            		ConfigSystem();
            		break;
            	case Operation.RunTests:
            		result = RunTest(args);
            		break;
            	case Operation.Invalid:
            	default:
            		ShowHelp();
            		break;
            }
            
            return result;
        }
        
        private static void ShowHelp()
        {
        }
        
        private static void ConfigSystem()
        {
        }
        
        static int RunTest(string[] args)
        {
            var testDiscovery = new NUnitTestDiscovery();

            var result = testDiscovery.FindTestAssembliesInPath(args[0]);

            Console.WriteLine(string.Format("Found {0} assemblies in path", result.Count()));
            Console.WriteLine();
            var commandLineCreator = new NUnitCommandLineMaker(Settings.Default.NUnitRunnerPath);
            var arguments = string.Join(" ", result.Select(path => "\"" + path + "\"").ToArray());

            Console.WriteLine("Running: {0} {1}", commandLineCreator.TestRunner, arguments);
            Console.WriteLine();
            var startInfo = new ProcessStartInfo(commandLineCreator.TestRunner, arguments)
                                {
                                    WorkingDirectory = args[0],
                                    RedirectStandardOutput = true,
                                    UseShellExecute = false
                                };

            var proc = Process.Start(startInfo);
            do
            {
                string output = proc.StandardOutput.ReadLine();
                Console.WriteLine(output);
            }
            while (proc.StandardOutput.EndOfStream == false);

            proc.WaitForExit();

            return proc.ExitCode;
        }
    }
}