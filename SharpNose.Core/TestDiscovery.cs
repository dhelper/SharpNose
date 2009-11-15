using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace SharpNose.Core
{
    public abstract class TestDiscovery
    {
        public IEnumerable<string> FindTestAssembliesInPath(string path)
        {
            return from filename in Directory.GetFiles(path)
                   where IsValidAssebly(filename)
                   let assembly = Assembly.LoadFrom(filename)
                   where IsTestAssembly(assembly)
                   select filename;
        }

        private static bool IsValidAssebly(string filename)
        {
            try
            {
                AssemblyName.GetAssemblyName(filename);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool IsTestAssembly(Assembly assembly)
        {
            if (GetAllTestClasses(assembly).Any())
            {
                return true;
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

        public abstract string TestFixtureName { get; }
    }
}