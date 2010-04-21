using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpNose
{
    public enum Operation
    {
        Invalid,
        Config,
        RunTests
    }

    public class ArgumentParser
    {
        public ArgumentParser(IEnumerable<string> arguments)
        {
            SelectedOperation = Operation.Invalid;

            if (arguments.Count() == 1)
            {
                string fullPath = Path.GetFullPath(arguments.First());
                if (Directory.Exists(fullPath))
                {
                    SelectedOperation = Operation.RunTests;
                }
            }
        }

        public Operation SelectedOperation { get; private set; }
    }
}