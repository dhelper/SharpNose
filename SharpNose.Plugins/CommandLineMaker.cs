using System.IO;

namespace SharpNose.SDK
{
    public abstract class CommandLineMaker
    {
        public abstract string TestRunner { get; }

        public static string GetFullPathWithExpandedEnvVariables(string path)
        {
            return Path.GetFullPath(System.Environment.ExpandEnvironmentVariables(path));
        }
    }
}