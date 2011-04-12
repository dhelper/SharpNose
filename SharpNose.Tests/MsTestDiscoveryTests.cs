using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpNose.SDK.MsTest;

namespace SharpNose.Tests
{
    [TestFixture]
    internal class MsTestDiscoveryTests : TestFrameworkTestBase
    {
        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssembly_FindOneFile()
        {
            MsTestTestDiscovery discovery =
                new MsTestTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(SimpleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FindTestAssembliesFromPath_PathHasTwoAssembly_FindTwoFiles()
        {
            MsTestTestDiscovery discovery =
                new MsTestTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(MultipleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
