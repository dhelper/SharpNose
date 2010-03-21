using NUnit.Framework;
using System.Linq;
using SharpNose.Core.NUnit;

namespace SharpNose.Tests
{
	[TestFixture]
    public class NUnitDiscoveryTests : NUnitTestBase
    {
        [Test]
        public void FindTestAssembliesFromPath_PathHasSingleAssembly_FindOneFile()
        {
            var discovery = new NUnitTestDiscovery();
			
            var result = discovery.FindTestAssembliesInPath(SimpleAssemblyPath);
			
            Assert.That(result.Count(), Is.EqualTo(1));
        }	
		
        [Test]
        public void  FindTestAssembliesFromPath_PathHasTwoAssembly_FindTwoFiles()
        {
            var discovery = new NUnitTestDiscovery();
			
            var result = discovery.FindTestAssembliesInPath(MultipleAssemblyPath);
			
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}