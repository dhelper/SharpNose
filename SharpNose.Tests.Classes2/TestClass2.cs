using NUnit.Framework;
using TypeMock;

namespace SharpNose.Tests.Classes2
{
    [TestFixture]
    public class TestClass2
    {
        public void DummyMethodUsedToKeepTypemockReference()
        {
            MockManager.Init();    
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(24, 6 * 4);
        }
		
        [Test]
        public void Test2()
        {
            Assert.IsTrue(true);
        }
		
        [Test]
        public void Test3()
        {
            Assert.IsFalse(false);
        }
    }
}