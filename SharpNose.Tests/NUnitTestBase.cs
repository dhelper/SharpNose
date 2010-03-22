using System;
using System.IO;
using NUnit.Framework;

namespace SharpNose.Tests
{
    public abstract class NUnitTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            if (Directory.Exists(SimpleAssemblyPath) == false)
            {
                Directory.CreateDirectory(SimpleAssemblyPath);
            }
            File.Copy(@".\" + SimpleAssemblyName, SimpleAssemblyFullName, true);

            if (Directory.Exists(MultipleAssemblyPath) == false)
            {
                Directory.CreateDirectory(MultipleAssemblyPath);
            }
            File.Copy(@".\" + SimpleAssemblyName, MultipleAssemblyPath + "\\" + SimpleAssemblyName, true);
            File.Copy(@".\" + SimpleAssembly2Name, MultipleAssemblyPath + "\\" + SimpleAssembly2Name, true);
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


        internal string SimpleAssemblyName
        {
            get
            {
                return "SharpNose.Tests.Classes.dll";
            }
        }

        internal string SimpleAssembly2Name
        {
            get
            {
                return "SharpNose.Tests.Classes2.dll";
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

        internal string SimpleAssemblyFullName
        {
            get
            {
                return SimpleAssemblyPath + "\\" + SimpleAssemblyName;
            }
        }
    }
}