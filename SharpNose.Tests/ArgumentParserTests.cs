using System;
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
			var parser = new ArgumentParser(new []{Assembly.GetExecutingAssembly().Location, Assembly.GetExecutingAssembly().Location});
			
			Assert.AreEqual(Operation.Invalid, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithValidPath_SelectedOperationRunTests()
		{
			var parser = new ArgumentParser(new[]{Environment.CurrentDirectory});
			
			Assert.AreEqual(Operation.RunTests, parser.SelectedOperation);
		}
		
		[Test]
		public void Parse_CreatedWithInValidPath_SelectedOperationInvalid()
		{
			var parser = new ArgumentParser(new[]{"DummyPath"});
			
			Assert.AreEqual(Operation.Invalid, parser.SelectedOperation);
		}
	}
}
