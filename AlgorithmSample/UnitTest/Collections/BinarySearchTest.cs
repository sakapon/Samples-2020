using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Collections;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Collections
{
	[TestClass]
	public class BinarySearchTest
	{
		[TestMethod]
		public void FirstGt_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 1000; n < 1010; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
				for (int x = -2; x < n + 2; x++)
				{
					var actual = BinarySearch.FirstGt(a, x);
					Assert.IsTrue(actual == n || a[actual] > x);
					Assert.IsTrue(actual == 0 || a[actual - 1] <= x);
				}
			}
		}

		[TestMethod]
		public void LastGt_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 1000; n < 1010; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.CreateData(n).OrderBy(x => -x).ToArray();
				for (int x = -2; x < n + 2; x++)
				{
					var actual = BinarySearch.LastGt(a, x);
					Assert.IsTrue(actual == -1 || a[actual] > x);
					Assert.IsTrue(actual == n - 1 || a[actual + 1] <= x);
				}
			}
		}
	}
}
