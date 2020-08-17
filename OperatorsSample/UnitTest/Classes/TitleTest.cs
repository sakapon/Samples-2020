using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Classes;

namespace UnitTest.Classes
{
	[TestClass]
	public class TitleTest
	{
		[TestMethod]
		public void Equals()
		{
			var v1 = new Title("Book", 2);
			var v2 = new Title("Book", 2);
			var v3 = new Title("Article", 3);
			var ec = EqualityComparer<Title>.Default;

			Assert.IsTrue(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsTrue((object)v1 == (object)v1);
			Assert.IsTrue(v1 == v1);

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));
			Assert.IsFalse((object)v1 == (object)v2);
			Assert.IsTrue(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
			Assert.IsFalse((object)v1 == (object)v3);
			Assert.IsFalse(v1 == v3);
		}

		[TestMethod]
		public void CompareTo()
		{
			var v1 = new Title("Book", 2);
			var v2 = new Title("Book", 2);
			var v3 = new Title("Article", 3);

			Assert.IsTrue(v3 < v1);
			Assert.IsFalse(v1 < v2);
			Assert.IsFalse(v1 < v3);

			Assert.IsFalse(v3 > v1);
			Assert.IsFalse(v1 > v2);
			Assert.IsTrue(v1 > v3);

			Assert.IsTrue(v3 <= v1);
			Assert.IsTrue(v1 <= v2);
			Assert.IsFalse(v1 <= v3);

			Assert.IsFalse(v3 >= v1);
			Assert.IsTrue(v1 >= v2);
			Assert.IsTrue(v1 >= v3);
		}

		[TestMethod]
		public void Equals_Null()
		{
			var v0 = default(Title);
			var v1 = new Title("Book", 2);

			Assert.IsTrue(Title.Equals(v0, v0));
			Assert.IsFalse(Title.Equals(v0, v1));
			Assert.IsFalse(Title.Equals(v1, v0));

			Assert.IsTrue(v0 == v0);
			Assert.IsFalse(v0 == v1);
			Assert.IsFalse(v1 == v0);
		}

		[TestMethod]
		public void CompareTo_Null()
		{
			var v0 = default(Title);
			var v1 = new Title("Book", 2);

			Assert.AreEqual(0, Title.Compare(v0, v0));
			Assert.AreEqual(-1, Title.Compare(v0, v1));
			Assert.AreEqual(1, Title.Compare(v1, v0));

			Assert.IsFalse(v0 < v0);
			Assert.IsTrue(v0 < v1);
			Assert.IsFalse(v1 < v0);

			Assert.IsTrue(v0 <= v0);
			Assert.IsTrue(v0 <= v1);
			Assert.IsFalse(v1 <= v0);
		}

		[TestMethod]
		public void Distinct()
		{
			var vs = Array.ConvertAll(new int[1000], _ => new Title("Book", 2));
			Assert.AreEqual(0, Array.IndexOf(vs, new Title("Book", 2)));
			Assert.AreEqual(1, vs.Distinct().Count());
			Assert.AreEqual(1, vs.ToHashSet().Count);
		}

		[TestMethod]
		public void Sort()
		{
			var titles = new[]
			{
				new Title("Book", 22),
				new Title("Book", 3),
				new Title("article", 111),
				new Title("Article", 22),
				new Title(null, -1),
				new Title("book", 111),
				new Title("article", 3),
			};

			var titles2 = titles.OrderBy(x => x).ToArray();
			Array.Sort(titles);
			CollectionAssert.AreEqual(titles, titles2);

			foreach (var title in titles)
				Console.WriteLine($"{title.Name} #{title.Number}");
		}
	}
}
