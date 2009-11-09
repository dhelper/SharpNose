using System.IO;
using NUnit.Framework;

namespace SharpNose.Tests
{
    public abstract class NUnitTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            if(Directory.Exists(SimpleAssemblyPath) == false)
            {
                Directory.CreateDirectory(SimpleAssemblyPath);
                File.Copy(@".\" + SimpleAssemblyName, SimpleAssemblyFullName, true);
            }
			
            if(Directory.Exists(MultipleAssemblyPath) == false)
            {
                Directory.CreateDirectory(MultipleAssemblyPath);
                File.Copy(@".\" + SimpleAssemblyName, MultipleAssemblyPath + "\\" + SimpleAssemblyName, true);
                File.Copy(@".\" + SimpleAssembly2Name, MultipleAssemblyPath + "\\" + SimpleAssembly2Name, true);
            }
        }
		
        [TestFixtureTearDown]
        public void TearDown()
        {
            if(Directory.Exists(SimpleAssemblyPath))
            {
                Directory.Delete(SimpleAssemblyPath, true);
            }
			
            if(Directory.Exists(MultipleAssemblyPath))
            {
                Directory.Delete(MultipleAssemblyPath, true);
            }
        }
			
		
        internal string SimpleAssemblyName
        {
            get
            {
                return "DotNetNose.Tests.Classes.dll";
            }
        }
		
        internal string SimpleAssembly2Name
        {
            get
            {
                return "DotNetNose.Tests.Classes2.dll";
            }
        }
		
        internal string SimpleAssemblyPath
        {
            get
            {
                return @"\TestClasses";
            }
        }
		
        internal string MultipleAssemblyPath
        {
            get
            {
                return @"\TestClasses2";
            }
        }
		
        internal string SimpleAssemblyFullName
        {
            get
            {
                return SimpleAssemblyPath + "\\" + SimpleAssemblyName;
            }
        }
    }
}