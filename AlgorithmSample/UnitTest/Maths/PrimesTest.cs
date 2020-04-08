using System;
using System.Linq;
using AlgorithmLib.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Maths
{
	[TestClass]
	public class PrimesTest
	{
		[TestMethod]
		public void Factorize()
		{
			for (int i = 1; i <= 100; i++) Test(i);

			Test(6983776800);
			Test(9311702400);
			Test(1000000000037); // 10^12
			Test(1000000000039);
			Test(1L << 40);
			Test((1L << 40) - 1);

			void Test(long n)
			{
				var actual = Primes.Factorize(n);
				Console.WriteLine(string.Join(" ", actual));
				Assert.AreEqual(n, actual.Aggregate(1L, (x, y) => x * y));
			}
		}

		[TestMethod]
		public void Divisors()
		{
			for (int i = 1; i <= 100; i++) Test(i);

			Test(6983776800);
			Test(9311702400);
			Test(1000000000037); // 10^12
			Test(1000000000039);
			Test(1L << 40);
			Test((1L << 40) - 1);

			void Test(long n)
			{
				var actual = Primes.Divisors(n);
				var length = Primes.Factorize(n).GroupBy(p => p).Aggregate(1L, (x, g) => x * (g.LongCount() + 1));
				Assert.AreEqual(length, actual.Length);
			}
		}

		[TestMethod]
		public void IsPrime()
		{
			for (int i = 1; i <= 100; i++) Test(i);

			Test(1000000000037); // 10^12
			Test(1000000000039);
			Test(1L << 40);
			Test((1L << 40) - 1);

			void Test(long n)
			{
				Assert.AreEqual(Primes.Factorize(n).Length == 1, Primes.IsPrime(n));
			}
		}

		[TestMethod]
		public void GetPrimes()
		{
			Console.WriteLine(string.Join(" ", Primes.GetPrimes(100)));

			Test(1, 0);
			Test(2, 1);
			Test(10, 4);
			Test(100, 25);
			Test(1000, 168);
			Test(10000, 1229);
			Test(100000, 9592);
			Test(1000000, 78498);
			Test(10000000, 664579);

			void Test(int n, int expected)
			{
				var actual = TestHelper.MeasureTime(() => Primes.GetPrimes(n));
				Assert.AreEqual(expected, actual.Length);
			}
		}

		[TestMethod]
		public void GetPrimes_Range()
		{
			Console.WriteLine(string.Join(" ", Primes.GetPrimes(998244000, 998245000)));
			Console.WriteLine(string.Join(" ", Primes.GetPrimes(1000000000, 1000001000)));
			Console.WriteLine(string.Join(" ", Primes.GetPrimes(1000000000000, 1000000001000)));
			Assert.AreEqual(1000, Primes.GetPrimes(22801763489, 22801787296).Length);

			var actual = TestHelper.MeasureTime(() => Primes.GetPrimes(1000000000000, 1000001000000));
			Console.WriteLine(actual.Length);
		}

		[TestMethod]
		public void GetPrimes_Range_All()
		{
			for (int i = 1; i <= 100; i++) Test(i);

			Test(100000);
			Test(1000000);

			void Test(int M)
			{
				CollectionAssert.AreEqual(Primes.GetPrimes(M).Select(p => (long)p).ToArray(), Primes.GetPrimes(1, M));
			}
		}
	}
}
