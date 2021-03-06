﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpNose.SDK.NUnit
{
    public class NUnitCommandLineMaker : CommandLineMaker
    {
        private readonly string runnerPath;

        public NUnitCommandLineMaker(string nunitPath)
        {
            runnerPath = GetFullPathWithExpandedEnvVariables(nunitPath);
        }

        public override string TestRunner
        {
            get { return runnerPath + "\\nunit-console.exe"; }
        }

        public string GenerateArguments(IEnumerable<string> testFixturesFound)
        {
            string arguments = string.Join(" ", testFixturesFound.Select(path => "\"" + path + "\"").ToArray());
            return arguments;
        }
    }
}