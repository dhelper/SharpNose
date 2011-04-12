using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpNose.SDK.NUnit;

namespace SharpNose.Tests
{
    [TestFixture]
    public class IsolatorNUnitDisocveryTest : TestFrameworkTestBase
    {
        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssemblyWithIsolatorReferecne_FindZeroFiles()
        {
            NUnitIsolatorTestDiscovery discovery =
                new NUnitIsolatorTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(MultipleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssemblyWithoutIsolatorReferecne_FindZeroFiles()
        {
            NUnitIsolatorTestDiscovery discovery = new NUnitIsolatorTestDiscovery();

            IEnumerable<string> result =
                discovery.FindTestAssembliesInValidAssemblies(GetValidDotNetAssembliesFromPath(SimpleAssemblyPath));

            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
