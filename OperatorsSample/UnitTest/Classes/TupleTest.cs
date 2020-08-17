using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Classes
{
	[TestClass]
	public class TupleTest
	{
		[TestMethod]
		public void Equals()
		{
			// == 演算子は定義されていませんが、Equals で各要素を比較できます。
			var v1 = Tuple.Create(3, 4);
			var v2 = Tuple.Create(3, 4);
			var v3 = Tuple.Create(4, 3);
			var ec = EqualityComparer<Tuple<int, int>>.Default;

			Assert.IsTrue(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));
			Assert.IsFalse(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
			Assert.IsFalse(v1 == v3);
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => Tuple.Create(3, 4));
			Assert.AreEqual(0, Array.IndexOf(vs, Tuple.Create(3, 4)));
			Assert.AreEqual(1, vs.Distinct().Count());
			Assert.AreEqual(1, vs.ToHashSet().Count);
		}

		[TestMethod]
		public void Sort()
		{
			var titles = new[]
			{
				Tuple.Create("Book", 22),
				Tuple.Create("Book", 3),
				Tuple.Create("article", 111),
				Tuple.Create("Article", 22),
				Tuple.Create(default(string), -1),
				Tuple.Create("book", 111),
				Tuple.Create("article", 3),
			};

			var titles2 = titles.OrderBy(x => x).ToArray();
			Array.Sort(titles);
			CollectionAssert.AreEqual(titles, titles2);

			foreach (var (name, number) in titles)
				Console.WriteLine($"{name} #{number}");
		}
	}
}
