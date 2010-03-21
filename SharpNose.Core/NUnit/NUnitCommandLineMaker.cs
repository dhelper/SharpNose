using System.Collections.Generic;
using System.Linq;

namespace SharpNose.Core.NUnit
{
    public class NUnitCommandLineMaker : CommandLineMaker
    {
        private readonly string m_path;
        public NUnitCommandLineMaker(string nunitPath) 
        {
            m_path = nunitPath;	
        }
		
        override public string TestRunner
        {
            get
            {
                return m_path +"\\nunit-console.exe";
            }
			
        }

        public string GenerateArguments(IEnumerable<string> testFixturesFound)
        {
            var arguments = string.Join(" ", testFixturesFound.Select(path => "\"" + path + "\"").ToArray());
            return arguments;
        }
    }
}