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
			var (a, b) = v;
			var t = ((double c, double d))v;

			Assert.AreEqual(v, (a, b));
			Assert.AreEqual(v, t);
			Assert.AreEqual((v.X, v.Y), (t.c, t.d));
		}
	}
}
