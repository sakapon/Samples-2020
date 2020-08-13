using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class Vector2Test
	{
		[TestMethod]
		public void Cast()
		{
			Vector2 v = (3, 4);
			var t = ((double a, double b))v;
			var (c, d) = v;

			Assert.AreEqual(v.X, t.a);
			Assert.AreEqual(v.Y, t.b);
			Assert.AreEqual(v.X, c);
			Assert.AreEqual(v.Y, d);
		}
	}
}
