namespace SharpNose.Core
{
    public class CommandLineInfo
    {
        public string TestRunner { get; private set; }
        public string Arguments { get; private set; }

        public CommandLineInfo(string testRunner, string arguments, string additionalArguments)
        {
            TestRunner = testRunner;
            Arguments = string.IsNullOrEmpty(additionalArguments) == false ? 
                string.Format("{0} {1}", arguments, additionalArguments) : arguments;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", TestRunner, Arguments);
        }
    }
}