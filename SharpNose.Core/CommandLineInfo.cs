namespace SharpNose.Core
{
    public class CommandLineInfo
    {
        public string TestRunner { get; private set; }
        public string Arguments { get; private set; }

        public CommandLineInfo(string testRunner, string arguments)
        {
            TestRunner = testRunner;
            Arguments = arguments;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", TestRunner, Arguments);
        }
    }
}