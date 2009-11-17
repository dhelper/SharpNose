using System;
using System.Diagnostics;
using NUnit.Framework;
using System.Reflection;

namespace SharpNose.Tests
{
	[TestFixture]
	public class ArgumentParserTests
	{
		[Test]
		public void Parse_CreatedWithNoArgs_SelectedOperationInvalid()
		{
			var parser = new ArgumentParser(new string[0]);
			
			Assert.AreEqual(Operation.Invalid, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithMoreThanOneArg_SelectedOperationInvalid()
		{
			var parser = new ArgumentParser(new []{Assembly.GetExecutingAssembly().Location.ToString(), Assembly.GetExecutingAssembly().Location.ToString()});
			
			Assert.AreEqual(Operation.Invalid, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithValidPath_SelectedOperationRunTests()
		{
			var parser = new ArgumentParser(new string[]{Environment.CurrentDirectory});
			
			Assert.AreEqual(Operation.RunTests, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithInValidPath_SelectedOperationInvalid()
		{
			var parser = new ArgumentParser(new string[]{"DummyPath"});
			
			Assert.AreEqual(Operation.Invalid, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithConfigCommand_SelectedOperationIsConfig()
		{
			var parser = new ArgumentParser(new string[]{"-config"});
			
			Assert.AreEqual(Operation.Config, parser.SelectedOperation);
		}	
		
	}
}
