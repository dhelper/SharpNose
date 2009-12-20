using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using SharpNose.Core;
using SharpNose.Properties;

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

            var result  = 0;
            var parser = new ArgumentParser(args);
            switch (parser.SelectedOperation) {
            	case Operation.Config:
            		if(ConfigSystem() == false)
            		{
            			result = 1;
            		}
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
            Console.WriteLine("Configure the path to NUnit using:");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("SharpNose -config");
            Console.WriteLine();
            Console.ForegroundColor = foregroundColor;
        }
        
        private static bool ConfigSystem()
        {
        	Console.WriteLine("Current NUnit directory: {0}", Settings.Default.NUnitRunnerPath);
        	Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please enter NUnit directory:");
        	var suggestedPath = System.Console.ReadLine();
        	if(Directory.Exists(suggestedPath) == false)
        	{
                Console.ForegroundColor = ConsoleColor.Red;
        		Console.WriteLine("Path not found - exiting");
        		return false;
        	}
        	        	
        	Settings.Default.NUnitRunnerPath = suggestedPath;
        	Settings.Default.Save();
        	return true;
        }

       

        static int RunTest(string[] args)
        {
            var testDiscovery = new NUnitTestDiscovery(Settings.Default.NUnitRunnerPath);

            var result = testDiscovery.FindTestAssembliesInPath(args[0]);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Found {0} assemblies in path", result.Count()));
            Console.WriteLine();

            var commandLineInfo = testDiscovery.GenerateCommandLine(result);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Running: {0}", commandLineInfo);
            Console.WriteLine();
            var startInfo = new ProcessStartInfo(commandLineInfo.TestRunner, commandLineInfo.Arguments)
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