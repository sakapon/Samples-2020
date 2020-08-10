using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Classes;

namespace UnitTest.Classes
{
	[TestClass]
	public class Vector0Test
	{
		[TestMethod]
		public void Equals()
		{
			// 各要素の比較はできません。
			var v1 = new Vector0(3, 4);
			var v2 = new Vector0(3, 4);
			var v3 = new Vector0(4, 3);

			Assert.IsTrue(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			// ReferenceEquals と同じです。
			Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsFalse(Equals(v1, v2));
			Assert.IsFalse(v1.Equals(v2));
			Assert.IsFalse(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(v1 == v3);
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => new Vector0(3, 4));
			Assert.AreEqual(-1, Array.IndexOf(vs, new Vector0(3, 4)));
			Assert.AreEqual(1000, vs.Distinct().Count());
			Assert.AreEqual(1000, vs.ToHashSet().Count);
		}
	}
}
