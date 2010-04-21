using System.Linq;
using NUnit.Framework;
using SharpNose.SDK.NUnit;

namespace SharpNose.Tests
{
    [TestFixture]
    public class IsolatorNUnitDisocveryTest : NUnitTestBase
    {
        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssemblyWithoutIsolatorReferecne_FindZeroFiles()
        {
            var discovery = new NUnitIsolatorTestDiscovery();

            var result = discovery.FindTestAssembliesInPath(SimpleAssemblyPath);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssemblyWithIsolatorReferecne_FindZeroFiles()
        {
            var discovery = new NUnitIsolatorTestDiscovery();

            var result = discovery.FindTestAssembliesInPath(MultipleAssemblyPath);

            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}
