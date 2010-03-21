using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SharpNose.Core.NUnit
{
    [Export(typeof(TestDiscovery))]
    public class NUnitTestDiscovery : TestDiscovery
    {
        public override string Name
        {
            get { return "NUnit"; }
        }

        protected override string TestFixtureName
        {
            get { return "TestFixtureAttribute"; }
        }

        public override CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            var commandlineMaker = new NUnitCommandLineMaker(TestRunnerPath);

            return new CommandLineInfo(commandlineMaker.TestRunner, commandlineMaker.GenerateArguments(testFixtruesFound));
        }
    }
}