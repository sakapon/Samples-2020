using System;
using System.Collections.Generic;
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
			var v1 = new Vector0(3, 4);
			var v2 = new Vector0(3, 4);
			var v3 = new Vector0(4, 3);

			Assert.IsFalse(ReferenceEquals(v1, v1));
			Assert.IsTrue(Equals(v1, v1));
			Assert.IsTrue(v1.Equals(v1));

			Assert.IsFalse(ReferenceEquals(v1, v2));
			Assert.IsTrue(Equals(v1, v2));
			Assert.IsTrue(v1.Equals(v2));

			Assert.IsFalse(ReferenceEquals(v1, v3));
			Assert.IsFalse(Equals(v1, v3));
			Assert.IsFalse(v1.Equals(v3));
		}
	}
}
