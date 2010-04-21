using System.Collections.Generic;

namespace SharpNose.Plugins
{
    public class PluginConfigurations
    {
        private readonly Dictionary<string, TestRunnerConfiguration> configurations =
            new Dictionary<string, TestRunnerConfiguration>();

        public TestRunnerConfiguration this[string pluginName]
        {
            get { return configurations[pluginName]; }
        }

        public void AddConfiguration(TestRunnerConfiguration configuration)
        {
            configurations.Add(configuration.Name, configuration);
        }
    }
}