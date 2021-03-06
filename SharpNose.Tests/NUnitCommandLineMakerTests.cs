using NUnit.Framework;
using SharpNose.SDK.NUnit;

namespace SharpNose.Tests
{
    [TestFixture]
    public class NUnitCommandLineMakerTests : TestFrameworkTestBase
    {
        [Test]
        public void GetCommandLine_PathHasSingleAssembly_CommandLineIncludeThatAssembly()
        {
            var clm = new NUnitCommandLineMaker("c:\\numitDummyPath");

            const string expected = "c:\\numitDummyPath\\nunit-console.exe";

            Assert.That(clm.TestRunner, Is.EqualTo(expected));
        }
    }
}