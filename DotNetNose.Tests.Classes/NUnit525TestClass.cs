using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DotNetNose.Tests.Classes
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