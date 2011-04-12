using NUnit.Framework;
using SharpNose.SDK;

namespace SharpNose.Tests
{
    [TestFixture]
    public class ValidDotNetAssembliesTests : ValidDotNetAssembliesTestBase
    {
        private const string BASE_PATH = "c:\\basepath";

        internal override ArgumentParser Args
        {
            get { return new ArgumentParserFake(new[] {BASE_PATH, "/r"}); }
        }

        [Test]
        public void GetValidDotNetAssembliesRecursively_PathHasTwelveFiles_FindsNineValidAssemblies()
        {
            Assert.AreEqual(9, this.GetValidDotNetAssemblies().Count);
        }

        [Test]
        public void GetValidDotNetAssembliesRecursively_NineValidAssemblies_AllStartWithBasePath()
        {
            var validAssemblies = this.GetValidDotNetAssemblies();
            foreach (string assembly in validAssemblies)
            {
                Assert.IsTrue(assembly.StartsWith(BASE_PATH));
            }
        }

        [Test]
        public void GetValidDotNetAssembliesRecursively_NineValidAssemblies_AllEndWithValid()
        {
            var validAssemblies = this.GetValidDotNetAssemblies();
            foreach (string assembly in validAssemblies)
            {
                Assert.IsTrue(assembly.EndsWith("valid"));
            }
        }
    }
}
