using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Classes
{
	[TestClass]
	public class StringTest
	{
		[TestMethod]
		public void Equals()
		{
			var v1 = "Book";
			var v2 = "Book";
			var v3 = "Article";
			var ec = EqualityComparer<string>.Default;

			Assert.IsTrue(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));
			Assert.IsTrue(ec.Equals(v1, v1));
			// ReferenceEquals と同じです。
			Assert.IsTrue((object)v1 == (object)v1);
			Assert.IsTrue(v1 == v1);

			// ！？
			Assert.IsTrue(ReferenceEquals(v1, v2));
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));
			Assert.IsTrue(ec.Equals(v1, v2));
			Assert.IsTrue((object)v1 == (object)v2);
			Assert.IsTrue(v1 == v2);

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
			Assert.IsFalse(ec.Equals(v1, v3));
			Assert.IsFalse((object)v1 == (object)v3);
			Assert.IsFalse(v1 == v3);
		}
	}
}
