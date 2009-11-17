using System;
using System.IO;

namespace SharpNose
{
	public enum Operation
	{
		Invalid,
		Config,
		RunTests
	}
	
	public class ArgumentParser
	{
		public ArgumentParser(string[] arguments)
		{
			SelectedOperation = Operation.Invalid;
			
			if(arguments.Length == 1)
			{
				if(Directory.Exists(arguments[0]))
				{
					SelectedOperation = Operation.RunTests;
				}
				
				if(arguments[0] == "-config")
				{
					SelectedOperation = Operation.Config;
					
				}
			}		
		}
		
		public Operation SelectedOperation{get; private set;}
	}
}
