namespace SharpNose.SDK
{
    public class CommandLineInfo
    {
        public CommandLineInfo(string testRunner, string arguments, string additionalArguments)
        {
            TestRunner = testRunner;
            Arguments = arguments;
            AddtionalArguments = additionalArguments;
        }

        public string TestRunner { get; private set; }
        public string Arguments { get; private set; }
        public string AddtionalArguments { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", TestRunner, Arguments, AddtionalArguments);
        }
    }
}