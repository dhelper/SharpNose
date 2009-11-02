using System;
using NUnit.Framework;

namespace DotNetNose.Tests.Classes2
{
	[TestFixture]
	public class TestClass2
	{
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
