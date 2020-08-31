using System;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class VectorInitTest
	{
		[TestMethod]
		public void Calculations()
		{
			// IL で default(T) と new T() は同一になるようです。
			Assert.AreEqual(new VectorInit(0, 0), default);
			Assert.AreEqual(new VectorInit(0, 0), new VectorInit());

			var v1 = new VectorInit(2, 2);
			var v2 = new VectorInit(3, 4);

			Assert2.AreNearlyEqual(2 * Math.Sqrt(2), v1.Norm);
			Assert.AreEqual(5, v2.Norm);
			Assert2.AreNearlyEqual(Math.PI / 4, v1.Angle);
			Assert2.AreNearlyEqual(4.0 / 3, Math.Tan(v2.Angle));
		}
	}
}
