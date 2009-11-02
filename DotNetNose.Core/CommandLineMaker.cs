using System;
using System.Linq;

namespace DotNetNose.Core
{
	abstract public class CommandLineMaker
	{
		 NUnitTestDiscovery testDiscovery;
		public CommandLineMaker()
		{
			testDiscovery = new NUnitTestDiscovery();
		}
		
		abstract public string TestRunner{get;}
		
        //public string GetCommandLine(string path)
        //{
        //    var assemblies = testDiscovery.FindTestAssembliesInPath(path);
        //    var fileList = string.Join(" ", assemblies.ToArray());
			
        //    var result = string.Format("{0} {1}", TestRunner, fileList);
			
        //    return result;
        //}
	}
	

}
