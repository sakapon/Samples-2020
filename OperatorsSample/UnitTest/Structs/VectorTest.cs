using System;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class VectorTest
	{
		[TestMethod]
		public void Operators()
		{
			var v1 = new Vector(2, 2);
			var v2 = new Vector(3, 4);
			var v3 = new Vector(18, 24);

			Assert.AreEqual(new Vector(5, 6), v1 + v2);
			Assert.AreEqual(-new Vector(1, 2), v1 - v2);
			Assert.AreEqual(new Vector(6, 8), 2 * v2);
			Assert.AreEqual(new Vector(6, 8), v3 / 3);
			Assert.AreEqual(14, v1 * v2);
		}

		[TestMethod]
		public void Others()
		{
			var v1 = new Vector(2, 2);
			var v2 = new Vector(3, 4);

			Assert2.AreNearlyEqual(2 * Math.Sqrt(2), v1.Norm);
			Assert.AreEqual(5, v2.Norm);
			Assert2.AreNearlyEqual(Math.PI / 4, v1.Angle);
			Assert2.AreNearlyEqual(4.0 / 3, Math.Tan(v2.Angle));
			Assert.AreEqual(1, Vector.Area(v1, v2));
		}
	}
}
