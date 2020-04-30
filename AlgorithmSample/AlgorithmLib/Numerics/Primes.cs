using System;
using System.Collections.Generic;

namespace AlgorithmLib.Numerics
{
	public static class Primes
	{
		/// <summary>
		/// 指定された自然数を素因数分解します。O(√n)
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>素因数のコレクション。n = 1 の場合は空の配列。</returns>
		public static long[] Factorize(long n)
		{
			var r = new List<long>();
			for (long x = 2; x * x <= n && n > 1; ++x)
				while (n % x == 0)
				{
					r.Add(x);
					n /= x;
				}

			// √n を超える素因数はたかだか 1 個であり、その次数は 1。
			if (n > 1) r.Add(n);
			return r.ToArray();
		}

		/// <summary>
		/// 指定された自然数の約数をすべて求めます。O(√n)
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>約数のコレクション。</returns>
		public static long[] Divisors(long n)
		{
			var r = new List<long>();
			for (long x = 1; x * x <= n; ++x)
				if (n % x == 0) r.Add(x);

			for (int i = r.Count - 1; i >= 0; --i)
			{
				var v = n / r[i];
				if (v > r[i]) r.Add(v);
			}
			return r.ToArray();
		}

		/// <summary>
		/// 指定された自然数が素数かどうかを判定します。O(√n)
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>素数である場合に限り true。</returns>
		public static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x)
				if (n % x == 0) return false;
			return n > 1;
		}

		/// <summary>
		/// 指定された自然数以下の素数をすべて求めます。およそ O(n)
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>素数のコレクション。</returns>
		public static long[] GetPrimes(long n)
		{
			var b = new bool[n + 1];
			for (long p = 2; p * p <= n; ++p)
				if (!b[p])
					for (long x = p * p; x <= n; x += p)
						b[x] = true;

			var r = new List<long>();
			for (long x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
			return r.ToArray();
		}

		/// <summary>
		/// 指定された範囲内の素数をすべて求めます。およそ O(max(√M, M - m))
		/// </summary>
		/// <param name="m">最小値。</param>
		/// <param name="M">最大値。</param>
		/// <returns>素数のコレクション。</returns>
		public static long[] GetPrimes(long m, long M)
		{
			var rM = (int)Math.Ceiling(Math.Sqrt(M));

			var b1 = new bool[rM + 1];
			var b2 = new bool[M - m + 1];
			if (m == 1) b2[0] = true;

			for (long p = 2; p <= rM; ++p)
				if (!b1[p])
				{
					for (var x = p * p; x <= rM; x += p) b1[x] = true;
					for (var x = Math.Max(p, (m + p - 1) / p) * p; x <= M; x += p) b2[x - m] = true;
				}

			var r = new List<long>();
			for (int x = 0; x < b2.Length; ++x) if (!b2[x]) r.Add(m + x);
			return r.ToArray();
		}
	}
}
