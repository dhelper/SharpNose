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
            var files = Directory.GetFiles(path);
            foreach(var filename in files)
            {
                if(IsValidAssebly(filename))
                {
                    var assembly = Assembly.LoadFrom(filename);
				   
                    if(IsTestAssembly(assembly))
                    {
                        yield return filename;
                    }
                }
            }
			
        }
		
        private bool IsValidAssebly(string filename)
        {
            try
            {
                AssemblyName.GetAssemblyName(filename);			
            }
            catch(Exception)
            {
                return false;
            }
			
            return true;
        }
		
        private bool IsTestAssembly(Assembly assembly)
        {
            if(GetAllTestClasses(assembly).Any())
            {
                return true;	
            }
			
            return false;
        }
		
        private IEnumerable<Type> GetAllTestClasses(Assembly assembly) 
        {
            foreach(Type type in assembly.GetTypes()) 
            {
                foreach(var attribute in  type.GetCustomAttributes(true))
                {
                    var attributeName = attribute.GetType().Name;
    				
                    if(attributeName.Equals(TestFixtureName))
                    {
                        yield return type;
                    }
                }
            }
        }
		
        public abstract string TestFixtureName{get;}
    }
}