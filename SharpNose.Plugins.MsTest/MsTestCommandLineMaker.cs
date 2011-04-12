using System.Collections.Generic;
using System.Linq;

namespace SharpNose.SDK.MsTest
{
    public class MsTestCommandLineMaker : CommandLineMaker
    {
        private readonly string _runnerPath;

        public MsTestCommandLineMaker(string runnerPath)
        {
            this._runnerPath = GetFullPathWithExpandedEnvVariables(runnerPath);
        }

        public override string TestRunner
        {
            get { return _runnerPath + "\\mstest.exe"; }
        }

        public string GenerateArguments(IEnumerable<string> testFixturesFound)
        {
            string arguments = string.Join(
                " ", testFixturesFound.Select(path => "/testcontainer:\"" + path + "\"").ToArray());
            return arguments;
        }
    }
}
