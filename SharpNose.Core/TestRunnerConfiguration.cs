namespace SharpNose.Core
{
    public class TestRunnerConfiguration
    {
        public TestRunnerConfiguration(string name, string path, string additionalArguments)
        {
            Name = name;
            Path = path;
            AdditionalArguments = additionalArguments;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string AdditionalArguments { get; private set; }
    }
}