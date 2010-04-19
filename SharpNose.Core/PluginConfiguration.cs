using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpNose.Core
{
    public class PluginConfigurations
    {
        private readonly Dictionary<string, TestRunnerConfiguration> configurations;

        public void AddConfiguration(TestRunnerConfiguration configuration)
        {
            configurations.Add(configuration.Name, configuration);
        }

        public TestRunnerConfiguration this[string pluginName]
        {
            get
            {
                return configurations[pluginName];
            }
        }
    }
}
