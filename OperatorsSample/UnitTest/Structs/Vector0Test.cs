using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class Vector0Test
	{
		[TestMethod]
		public void Equals()
		{
			// == 演算子は定義されていませんが、Equals で各要素を比較できます。
			var v1 = new Vector0(3, 4);
			var v2 = new Vector0(3, 4);
			var v3 = new Vector0(4, 3);
			var ec = EqualityComparer<Vector0>.Default;

			// 値型では、参照としての比較結果はつねに false です。
			Assert.IsFalse(ReferenceEquals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsFalse((object)v1 == (object)v1);
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));
			// コンパイル エラー。
			//Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsFalse((object)v1 == (object)v2);
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse((object)v1 == (object)v3);
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => new Vector0(3, 4));
			Assert.AreEqual(0, Array.IndexOf(vs, new Vector0(3, 4)));
			Assert.AreEqual(1, vs.Distinct().Count());
			Assert.AreEqual(1, vs.ToHashSet().Count);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Sort()
		{
			var titles = new[]
			{
				new Vector0(3, 4),
				new Vector0(3, 4),
			};

			Array.Sort(titles);
		}

		[TestMethod]
		public void KeyValuePair_Deconstruct()
		{
			var d = Enumerable.Range(1, 100).ToDictionary(i => i, i => i / 2.0);
			foreach (var (i, value) in d)
				Console.WriteLine($"{i} {value}");
		}
	}
}
