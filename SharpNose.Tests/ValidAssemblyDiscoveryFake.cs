using SharpNose.SDK;

namespace SharpNose.Tests
{
    public class ValidAssemblyDiscoveryFake : ValidAssemblyDiscovery
    {
        private const string VALID = "valid";
        private const string BOGUS = "bogus";

        public ValidAssemblyDiscoveryFake(ArgumentParser args)
            : base(args)
        {
        }

        internal override bool IsValidAssembly(string filename)
        {
            return filename.EndsWith(VALID);
        }

        internal override string[] GetDirectoriesInPath(string path)
        {
            return
                path.EndsWith(VALID)
                    ? new string[0]
                    : new[]
                          {
                              path + "\\" + "First" + VALID,
                              path + "\\" + "Second" + VALID,
                              path + "\\" + "Third" + VALID
                          };
        }

        internal override string[] GetFilesInPath(string path)
        {
            return
                path.EndsWith(VALID)
                    ? new[] { path + "\\" + "One" + VALID, path + "\\" + "two" + VALID, path + "\\" + "three" + VALID }
                    : new[] { path + "\\" + "four" + BOGUS, path + "\\" + "five" + BOGUS, path + "\\" + "six" + BOGUS };
        }
    }
}
