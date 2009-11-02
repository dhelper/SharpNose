using System;
using DotNetNose.Core;
using NUnit.Framework;
using System.IO;

namespace DotNetNose.Tests
{
	[TestFixture]
	public class NUnitCommandLineMakerTests : NUnitTestBase
	{
        [Test]
        public void GetCommandLine_PathHasSingleAssembly_CommandLineIncludeThatAssembly()
        {
            var clm = new NUnitCommandLineMaker("c:\\numitDummyPath");

            var expected = "c:\\numitDummyPath\\nunit-console.exe";

            Assert.That(clm.TestRunner, Is.EqualTo(expected));
        }
	}
}
