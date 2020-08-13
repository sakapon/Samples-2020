using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class VectorBoxingTest
	{
		[TestMethod]
		public void Equals()
		{
			var v1 = new VectorBoxing(3, 4);
			var v2 = new VectorBoxing(3, 4);
			var v3 = new VectorBoxing(4, 3);

			Assert.IsFalse(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			// ReferenceEquals と同じです。
			Assert.IsFalse((object)v1 == (object)v1);
			Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsFalse((object)v1 == (object)v2);
			Assert.IsTrue(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse((object)v1 == (object)v3);
			Assert.IsFalse(v1 == v3);
		}
	}
}
