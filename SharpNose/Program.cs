﻿using System;
using System.Diagnostics;
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
            		ShowHelp();
            		break;
            }

            Console.WriteLine("Goto www.Typemock.com to learn more about unit testing and TDD");

            return result;
        }
        
        private static void ShowHelp()
        {
            Console.WriteLine("Using #Nose is simple:");
            Console.WriteLine("SharpNose [TragetDirectory]");
            Console.WriteLine();
            Console.WriteLine("To configure the path to NUnit use:");
            Console.WriteLine("SharpNose -config");
            Console.WriteLine();
        }
        
        private static bool ConfigSystem()
        {
        	System.Console.WriteLine("Current NUnit directory: {0}", Settings.Default.NUnitRunnerPath);
        	System.Console.WriteLine("Please enter NUnit directory:");
        	var suggestedPath = System.Console.ReadLine();
        	if(Directory.Exists(suggestedPath) == false)
        	{
        		System.Console.WriteLine("Path not found - exiting");
        		return false;
        	}
        	        	
        	Settings.Default.NUnitRunnerPath = suggestedPath;
        	Settings.Default.Save();
        	return true;
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