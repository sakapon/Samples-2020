using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Collections;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Collections
{
	[TestClass]
	public class ComparerHelperTest
	{
		[TestMethod]
		public void Create()
		{
			var n = 10000;
			var expected = RandomHelper.CreateData(n);
			var actual = (int[])expected.Clone();

			Array.Sort(expected);
			Array.Sort(actual, ComparerHelper<int>.Create());
			CollectionAssert.AreEqual(expected, actual);

			Array.Reverse(expected);
			Array.Sort(actual, ComparerHelper<int>.Create(true));
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Create_Key1()
		{
			var n = 10000;
			var a = RandomHelper.CreateData(n);
			var actual = (int[])a.Clone();

			var expected = a.OrderBy(x => x.ToString()).ToArray();
			Array.Sort(actual, ComparerHelper<int>.Create(x => x.ToString()));
			CollectionAssert.AreEqual(expected, actual);

			Array.Reverse(expected);
			Array.Sort(actual, ComparerHelper<int>.Create(x => x.ToString(), true));
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Create_Key2()
		{
			var n = 10000;
			var a = RandomHelper.CreateData(n);
			var actual = (int[])a.Clone();

			var expected = a.OrderBy(x => x.ToString().Length).ThenByDescending(x => x).ToArray();
			Array.Sort(actual, ComparerHelper<int>.Create(x => x.ToString().Length, false, x => x, true));
			CollectionAssert.AreEqual(expected, actual);

			Array.Reverse(expected);
			Array.Sort(actual, ComparerHelper<int>.Create(x => x.ToString().Length, true, x => x, false));
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
