using System.Collections.ObjectModel;

namespace SharpNose.SDK
{
    public class ValidDotNetAssemblies : ReadOnlyCollection<string>
    {
        public readonly string BasePath;

        public ValidDotNetAssemblies(ValidAssemblyDiscovery discovery)
            : base(discovery.Assemblies)
        {
            BasePath = discovery.BasePath;
        }
    }
}
