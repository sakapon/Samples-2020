using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Classes
{
	[TestClass]
	public class AnonymousTest
	{
		[TestMethod]
		public void Equals()
		{
			// == 演算子は定義されていませんが、Equals で各要素を比較できます。
			var v1 = new { x = 3, y = 4 };
			var v2 = new { x = 3, y = 4 };
			var v3 = new { x = 4, y = 3 };
			EqualityComparer<T> GetEqualityComparer<T>(T o) => EqualityComparer<T>.Default;
			var ec = GetEqualityComparer(v1);

			Assert.IsTrue(ReferenceEquals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsTrue(v1 == v1);
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsFalse(v1 == v2);
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(v1 == v3);
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => new { x = 3, y = 4 });
			Assert.AreEqual(0, Array.IndexOf(vs, new { x = 3, y = 4 }));
			Assert.AreEqual(1, vs.Distinct().Count());
			Assert.AreEqual(1, vs.ToHashSet().Count);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Sort()
		{
			var titles = new[]
			{
				new { x = 3, y = 4 },
				new { x = 3, y = 4 },
			};

			Array.Sort(titles);
		}
	}
}
