using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpNose.Core.NUnit
{
    public class NUnitCommandLineMaker : CommandLineMaker
    {
        private readonly string runnerPath;
        public NUnitCommandLineMaker(string nunitPath) 
        {
            runnerPath = Path.GetFullPath(nunitPath);	
        }
		
        override public string TestRunner
        {
            get
            {
                return runnerPath +"\\nunit-console.exe";
            }
			
        }

        public string GenerateArguments(IEnumerable<string> testFixturesFound)
        {
            var arguments = string.Join(" ", testFixturesFound.Select(path => "\"" + path + "\"").ToArray());
            return arguments;
        }
    }
}