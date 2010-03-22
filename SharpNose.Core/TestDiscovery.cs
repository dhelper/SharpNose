using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace SharpNose.Core
{
    public abstract class TestDiscovery
    {
        abstract public string Name { get; }
        public abstract string TestFixtureName { get; }

        public abstract CommandLineInfo GenerateCommandLine(IEnumerable<string> testFixtruesFound);

        public string TestRunnerPath
        {
            get
            {
                return Configuration[Name].Path;
            }
        }

        public string AdditionalArguments
        {
            get
            {
                return Configuration[Name].AdditionalArguments;
            }
        }

        public virtual Dictionary<string, TestRunnerConfiguration> Configuration { get; set; }

        public virtual bool ShouldTestAssembly(Assembly assembly)
        {
            return true;
        }

        public IEnumerable<string> FindTestAssembliesInPath(string path)
        {
            return from filename in Directory.GetFiles(path)
                   where IsValidAssebly(filename)
                   let assembly = LoadAssembly(filename)
                   where IsTestAssembly(assembly)
                   select filename;
        }

        private static Assembly LoadAssembly(string filename)
        {
            try
            {
                return Assembly.LoadFrom(filename);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static bool IsValidAssebly(string filename)
        {
            try
            {
                AssemblyName.GetAssemblyName(filename);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private bool IsTestAssembly(Assembly assembly)
        {
            try
            {
                if (assembly == null)
                {
                    return false;
                }

                if (GetAllTestClasses(assembly).Any() && ShouldTestAssembly(assembly))
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