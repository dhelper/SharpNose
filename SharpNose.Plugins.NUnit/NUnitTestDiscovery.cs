using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

    public class NUnitIsolatorTestDiscovery : TestDiscovery
    {
        private readonly NUnitTestDiscovery internalTestDiscovery;

        public NUnitIsolatorTestDiscovery()
        {
            internalTestDiscovery = new NUnitTestDiscovery();
        }

        public override string Name
        {
            get
            {
                return "Isolator";
            }
        }


        public override string TestFixtureName
        {
            get { return internalTestDiscovery.TestFixtureName; }
        }

        public override CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            var result = internalTestDiscovery.GenerateCommandLine(testFixtruesFound);

            return new CommandLineInfo(Path.Combine(TestRunnerPath, "TMockRunner.exe"), result.TestRunner + " " + result.Arguments, AdditionalArguments);
        }

        public override bool ShouldTestAssembly(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Any(
                referencedAssembly => referencedAssembly.Name.Equals("TypeMock") ||
                    referencedAssembly.Name.Equals("Typemock.ArrangeActAssert"));
        }
    }
}