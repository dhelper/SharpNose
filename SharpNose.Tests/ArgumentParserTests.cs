using System;
using NUnit.Framework;
using System.Reflection;
using SharpNose.SDK;

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
		public void Parse_CreatedWithMoreThanOnePathArg_SelectedOperationInvalid()
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

        [Test]
        public void Parse_RecursiveArgSet_RecursiveTrue()
        {
            var parser = new ArgumentParser(new[] {"/r"});

            Assert.IsTrue(parser.RecursiveSearch);
        }

        [Test]
        public void Parse_MaskArgSet_MaskAssemblyTrue()
        {
            var parser = new ArgumentParser(new[] { "/m:Mask" });

            Assert.IsTrue(parser.MaskAssembly);
        }

        [Test]
        public void Parse_NoArgSet_MaskAssemblyAndRecursiveFalse()
        {
            var parser = new ArgumentParser(new string[]{});

            Assert.IsFalse(parser.MaskAssembly);
            Assert.IsFalse(parser.RecursiveSearch);
        }

        [Test]
        public void Parse_MaskArgSet_VerifyAssemblyMask()
        {
            var parser = new ArgumentParser(new[]{"/m:ThisIsTheMask" });

            Assert.AreEqual("ThisIsTheMask", parser.AssemblyMask.ToString());
        }
	}
}
