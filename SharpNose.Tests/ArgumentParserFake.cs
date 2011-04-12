using System.Collections.Generic;
using SharpNose.SDK;

namespace SharpNose.Tests
{
    public class ArgumentParserFake : ArgumentParser
    {
        public ArgumentParserFake(IEnumerable<string> arguments) : base(arguments)
        {
        }

        internal override string GetFullPath(string path)
        {
            return path;
        }

        internal override bool DirectoryExists(string path)
        {
            return true;
        }
    }
}
