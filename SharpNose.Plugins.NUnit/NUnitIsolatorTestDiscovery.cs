using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpNose.SDK.NUnit
{
    public class NUnitIsolatorTestDiscovery : TestDiscovery
    {
        private readonly NUnitTestDiscovery internalTestDiscovery;

        public NUnitIsolatorTestDiscovery()
        {
            internalTestDiscovery = new NUnitTestDiscovery();
        }

        public override string Name
        {
            get { return "Isolator"; }
        }


        public override string TestFixtureName
        {
            get { return internalTestDiscovery.TestFixtureName; }
        }

        public override CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            internalTestDiscovery.Configurations = this.Configurations;

            CommandLineInfo result = internalTestDiscovery.GenerateCommandLine(testFixtruesFound);

            return new CommandLineInfo(Path.Combine(TestRunnerPath, "TMockRunner.exe"),
                                       result.TestRunner + " " + result.Arguments, AdditionalArguments);
        }

        public override bool ShouldTestAssembly(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Any(
                referencedAssembly => referencedAssembly.Name.Equals("TypeMock") ||
                                      referencedAssembly.Name.Equals("Typemock.ArrangeActAssert"));
        }
    }
}