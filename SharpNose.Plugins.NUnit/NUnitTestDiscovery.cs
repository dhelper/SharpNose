using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpNose.Plugins;
using SharpNose.Plugins.NUnit;

namespace SharpNose.Core.NUnit
{
    public class NUnitTestDiscovery : TestDiscovery
    {
        public override string Name
        {
            get { return "NUnit"; }
        }

        public override string TestFixtureName
        {
            get { return "TestFixtureAttribute"; }
        }

        public override CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            var commandlineMaker = new NUnitCommandLineMaker(TestRunnerPath);

            string generateArguments = commandlineMaker.GenerateArguments(testFixtruesFound);

            return new CommandLineInfo(commandlineMaker.TestRunner, generateArguments, AdditionalArguments);
        }

        public override bool ShouldTestAssembly(Assembly assembly)
        {
            return !assembly.GetReferencedAssemblies().Any(
                referencedAssembly => referencedAssembly.Name.Equals("TypeMock") ||
                                      referencedAssembly.Name.Equals("Typemock.ArrangeActAssert"));
        }
    }
}