using System.Collections.Generic;

namespace SharpNose.Core
{
    public class NUnitTestDiscovery : TestDiscovery
    {
        private readonly NUnitCommandLineMaker commandlineMaker;
        public NUnitTestDiscovery(string runnerPath)
        {
            commandlineMaker = new NUnitCommandLineMaker(runnerPath);
        }

        protected override string TestFixtureName
        {
            get
            {
                return "TestFixtureAttribute";
            }
        }

        public CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            return new CommandLineInfo(commandlineMaker.TestRunner, commandlineMaker.GenerateArguments(testFixtruesFound));
        }
    }
}