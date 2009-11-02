using System;
using NUnit.Framework;


namespace DotNetNose.Tests.Classes2
{
	[TestFixture]
	public class TestClass1
	{
		[Test]
		public void Test1()
		{
			Assert.AreEqual(42, 6 * 7);
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