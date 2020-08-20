using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Structs
{
	[TestClass]
	public class ValueTupleTest
	{
		[TestMethod]
		public void Equals()
		{
			// == および != 演算子が使えます。(C# 7.3 以降)
			// フィールド名は無視されます。
			var v1 = (x: 3, y: 4);
			var v2 = (y: 3, x: 4);
			var v3 = (4, 3);
			var ec = EqualityComparer<(int, int)>.Default;

			Assert.IsFalse(ReferenceEquals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsFalse((object)v1 == (object)v1);
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));
			Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsFalse((object)v1 == (object)v2);
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));
			Assert.IsTrue(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse((object)v1 == (object)v3);
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
			Assert.IsFalse(v1 == v3);
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => (3, 4));
			Assert.AreEqual(0, Array.IndexOf(vs, (3, 4)));
			Assert.AreEqual(1, vs.Distinct().Count());
			Assert.AreEqual(1, vs.ToHashSet().Count);
		}

		[TestMethod]
		public void Sort()
		{
			var titles = new[]
			{
				("Book", 22),
				("Book", 3),
				("article", 111),
				("Article", 22),
				(null, -1),
				("book", 111),
				("article", 3),
			};

			var titles2 = titles.OrderBy(x => x).ToArray();
			Array.Sort(titles);
			CollectionAssert.AreEqual(titles, titles2);

			foreach (var (name, number) in titles)
				Console.WriteLine($"{name} #{number}");
		}
	}
}
