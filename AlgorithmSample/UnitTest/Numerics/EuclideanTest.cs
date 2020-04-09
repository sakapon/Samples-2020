using System;
using AlgorithmLib.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Numerics
{
	[TestClass]
	public class EuclideanTest
	{
		[TestMethod]
		public void Gcd()
		{
			for (int i = 1; i <= 100; i++)
				for (int j = 1; j <= 200; j++)
					Test(i, j);

			void Test(int x, int y)
			{
				var actual = Euclidean.Gcd(x, y);
				Assert.IsTrue(x % actual == 0 && y % actual == 0);
				for (int i = Math.Min(x, y); i > actual; i--)
					Assert.IsFalse(x % i == 0 && y % i == 0);
			}
		}

		[TestMethod]
		public void Lcm()
		{
			for (int i = 1; i <= 100; i++)
				for (int j = 1; j <= 150; j++)
					Test(i, j);

			void Test(int x, int y)
			{
				var actual = Euclidean.Lcm(x, y);
				Assert.IsTrue(actual % x == 0 && actual % y == 0);
				for (int i = Math.Max(x, y); i < actual; i++)
					Assert.IsFalse(i % x == 0 && i % y == 0);
			}
		}

		[TestMethod]
		public void ExtendedEuclid()
		{
			for (int i = 1; i <= 100; i++)
				for (int j = i + 1; j <= 100; j++)
					Test(i, j);

			void Test(long a, long b)
			{
				var g = Euclidean.Gcd(a, b);
				a /= g; b /= g;
				var (x, y) = Euclidean.ExtendedEuclid(a, b);
				Assert.IsTrue(Math.Abs(x) <= b);
				Assert.IsTrue(Math.Abs(y) <= a);
				Assert.AreEqual(1, a * x + b * y);
			}
		}
	}
}
