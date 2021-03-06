using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpNose.SDK.NUnit;

namespace SharpNose.Tests
{
    [TestFixture]
    public class NUnitDiscoveryTests : TestFrameworkTestBase
    {
        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssembly_FindOneFile()
        {
            NUnitTestDiscovery discovery =
                new NUnitTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(SimpleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FindTestAssembliesFromPath_PathHasTwoAssembly_FindOneFile()
        {
            NUnitTestDiscovery discovery =
                new NUnitTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(MultipleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}