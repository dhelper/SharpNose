using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;

namespace SharpNose.SDK
{
    [InheritedExport]
    public abstract class TestDiscovery
    {
        [Import("Configurations")]
        public PluginConfigurations Configurations { get; set; }

        public abstract string Name { get; }
        public abstract string TestFixtureName { get; }


        public string TestRunnerPath
        {
            get { return Configurations[Name].Path; }
        }

        public string AdditionalArguments
        {
            get { return Configurations[Name].AdditionalArguments; }
        }

        public abstract CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound);

        public virtual bool ShouldTestAssembly(Assembly assembly)
        {
            return true;
        }

        public IEnumerable<string> FindTestAssembliesInValidAssemblies(ValidDotNetAssemblies validAssemblies)
        {
            List<string> testAssemblies =
                (
                    from filename in validAssemblies
                    let assembly = LoadAssembly(filename)
                    where IsTestAssembly(assembly)
                    select filename
                ).ToList();

            return testAssemblies;
        }

        private static Assembly LoadAssembly(string filename)
        {
            ProgressingOrSpinningCursor.ProgressOrSpinCursor();

            try
            {
                return Assembly.LoadFrom(filename);
            }
            catch (Exception)
            {
                return null;
            }
        }


        private bool IsTestAssembly(Assembly assembly)
        {
            try
            {
                if (assembly == null)
                {
                    return false;
                }

                if (GetAllTestClasses(assembly).Any()
                    && ShouldTestAssembly(assembly))
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        private IEnumerable<Type> GetAllTestClasses(Assembly assembly)
        {
            return from type in assembly.GetTypes()
                   from attribute in type.GetCustomAttributes(true)
                   let attributeName = attribute.GetType().Name
                   where attributeName.Equals(TestFixtureName)
                   select type;
        }
    }
}