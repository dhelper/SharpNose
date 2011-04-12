using System.Collections.Generic;

namespace SharpNose.SDK.MsTest
{
    public class MsTestTestDiscovery : TestDiscovery
    {
        public override string Name
        {
            get { return "MsTest"; }
        }

        public override string TestFixtureName
        {
            get { return "TestClassAttribute"; }
        }

        public override CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound)
        {
            MsTestCommandLineMaker commandlineMaker = new MsTestCommandLineMaker(TestRunnerPath);

            string generateArguments = commandlineMaker.GenerateArguments(testFixtruesFound);

            return new CommandLineInfo(commandlineMaker.TestRunner, generateArguments, AdditionalArguments);
        }
    }
}
