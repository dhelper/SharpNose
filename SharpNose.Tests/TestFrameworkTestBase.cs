using System;
using System.IO;
using NUnit.Framework;
using SharpNose.SDK;

namespace SharpNose.Tests
{
    public abstract class TestFrameworkTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            if (Directory.Exists(SimpleAssemblyPath) == false)
            {
                Directory.CreateDirectory(SimpleAssemblyPath);
            }
            File.Copy(@".\" + NunitSimpleAssemblyName, NunitSimpleAssemblyFullName, true);
            File.Copy(@".\" + MsTestSimpleAssemblyName, MsTestSimpleAssemblyFullName, true);

            if (Directory.Exists(MultipleAssemblyPath) == false)
            {
                Directory.CreateDirectory(MultipleAssemblyPath);
            }
            File.Copy(@".\" + NunitSimpleAssemblyName, MultipleAssemblyPath + "\\" + NunitSimpleAssemblyName, true);
            File.Copy(@".\" + NunitSimpleAssembly2Name, MultipleAssemblyPath + "\\" + NunitSimpleAssembly2Name, true);
            File.Copy(@".\" + MsTestSimpleAssemblyName, MultipleAssemblyPath + "\\" + MsTestSimpleAssemblyName, true);
            File.Copy(@".\" + MsTestSimpleAssembly2Name, MultipleAssemblyPath + "\\" + MsTestSimpleAssembly2Name, true);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (Directory.Exists(SimpleAssemblyPath))
            {
                try
                {
                    Directory.Delete(SimpleAssemblyPath, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (Directory.Exists(MultipleAssemblyPath))
            {
                try
                {
                    Directory.Delete(MultipleAssemblyPath, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        internal ValidDotNetAssemblies GetValidDotNetAssembliesFromPath(string path)
        {
            return
                new ValidDotNetAssemblies(
                    new ValidAssemblyDiscovery(
                        new ArgumentParser(
                            new[] {path})
                        )
                    );
        }

        internal string NunitSimpleAssemblyName
        {
            get
            {
                return "SharpNose.Tests.Classes.dll";
            }
        }

        internal string MsTestSimpleAssemblyName
        {
            get
            {
                return "SharpNose.Tests.MsTest.Classes.dll";
            }
        }

        internal string NunitSimpleAssembly2Name
        {
            get
            {
                return "SharpNose.Tests.Classes2.dll";
            }
        }

        internal string MsTestSimpleAssembly2Name
        {
            get
            {
                return "SharpNose.Tests.MsTest.Classes2.dll";
            }
        }

        internal string SimpleAssemblyPath
        {
            get
            {
                return Path.GetTempPath() + @"\TestClasses";
            }
        }

        internal string MultipleAssemblyPath
        {
            get
            {
                return Path.GetTempPath() + @"\TestClasses2";
            }
        }

        internal string NunitSimpleAssemblyFullName
        {
            get
            {
                return SimpleAssemblyPath + "\\" + NunitSimpleAssemblyName;
            }
        }

        internal string MsTestSimpleAssemblyFullName
        {
            get
            {
                return SimpleAssemblyPath + "\\" + MsTestSimpleAssemblyName;
            }
        }
    }
}
