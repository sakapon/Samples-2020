using System;
using System.Collections.Generic;

namespace AlgorithmLab.Numerics
{
	public static class Primes
	{
		// O(√n)
		public static long[] Divisors(long n)
		{
			// n が大きい場合、誤差が生じる可能性があります。
			var rn = (long)Math.Ceiling(Math.Sqrt(n));

			var r = new List<long>();
			for (long x = 1; x <= rn; ++x)
			{
				if (n % x == 0) r.Add(x);
			}
			for (int i = r.Count - 1; i >= 0; --i)
			{
				var v = n / r[i];
				if (v > rn) r.Add(v);
			}
			return r.ToArray();
		}

		// O(√n)
		public static bool IsPrime_Bad(long n)
		{
			// n が大きい場合、誤差が生じる可能性があります。
			var rn = (long)Math.Ceiling(Math.Sqrt(n));

			// 候補 x を 2 または奇数に限定することで高速化できます。
			for (long x = 2; x <= rn; ++x)
				if (n % x == 0) return false;
			return n > 1;
		}

		// O(n)?
		public static int[] GetPrimes(int n)
		{
			// 候補 x を奇数に限定することで高速化できます。
			var b = new bool[n + 1];

			for (int p = 2; p * p <= n; ++p)
				if (!b[p])
					for (int x = p * p; x <= n; x += p)
						b[x] = true;

			var r = new List<int>();
			for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
			return r.ToArray();
		}

		// O(n)?
		public static int[] GetPrimesF(int n)
		{
			var b = PrimeFlags(n);
			var r = new List<int>();
			for (int x = 2; x <= n; ++x) if (b[x]) r.Add(x);
			return r.ToArray();
		}
		public static bool[] PrimeFlags(int n)
		{
			// 候補 x を奇数に限定することで高速化できます。
			var b = new bool[n + 1];
			for (int x = 2; x <= n; ++x) b[x] = true;

			for (int p = 2; p * p <= n; ++p)
				if (b[p])
					for (int x = p * p; x <= n; x += p)
						b[x] = false;
			return b;
		}

		// O(n)?
		public static long[] GetPrimesL(long n)
		{
			// 候補 x を奇数に限定することで高速化できます。
			var r = new List<long>();
			for (long x = 2; x <= n; ++x)
			{
				var b = true;
				foreach (var p in r)
				{
					if (p * p > x) break;
					if (x % p == 0) { b = false; break; }
				}
				if (b) r.Add(x);
			}
			return r.ToArray();
		}

		// O((M - m) √M)
		public static long[] GetPrimesL(long m, long M)
		{
			// n が大きい場合、誤差が生じる可能性があります。
			var rn = (int)Math.Ceiling(Math.Sqrt(M));
			var ps = GetPrimes(rn);

			// 候補 x を奇数に限定することで高速化できます。
			var r = new List<long>();
			foreach (var p in ps)
				if (p >= m) r.Add(p);
			for (var x = Math.Max(m, rn + 1); x <= M; ++x)
			{
				var b = true;
				foreach (var p in ps)
					if (x % p == 0) { b = false; break; }
				if (b) r.Add(x);
			}
			return r.ToArray();
		}
	}
}
