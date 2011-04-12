using SharpNose.SDK;

namespace SharpNose.Tests
{
    public abstract class ValidDotNetAssembliesTestBase
    {
        internal abstract ArgumentParser Args { get; }

        internal ValidDotNetAssemblies GetValidDotNetAssemblies()
        {
            return new ValidDotNetAssemblies(new ValidAssemblyDiscoveryFake(Args));
        }
    }
}
