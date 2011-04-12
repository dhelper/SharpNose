using NUnit.Framework;
using SharpNose.SDK.MsTest;

namespace SharpNose.Tests
{
    [TestFixture]
    public class MsTestCommandLineMakerTests
    {
        [Test]
        public void GetCommandLine_PathHasSingleAssembly_CommandLineIncludeThatAssembly()
        {
            MsTestCommandLineMaker clm = new MsTestCommandLineMaker("c:\\numitDummyPath");

            const string expected = "c:\\numitDummyPath\\mstest.exe";

            Assert.That(clm.TestRunner, Is.EqualTo(expected));
        }
    }
}
