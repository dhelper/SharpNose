using NUnit.Framework;

namespace SharpNose.Tests.Classes
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void Test()
        {
            Assert.AreEqual(4, 2 + 2);
        }
    }
}