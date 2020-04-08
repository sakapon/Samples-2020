using System;
using System.Collections.Generic;

namespace AlgorithmLib.Maths
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
			// √n を超える素因数はたかだか 1 個であり、その次数は 1。
			// 候補 x を 2 または奇数に限定することで高速化します。
			var r = new List<long>();
			while (n % 2 == 0) { r.Add(2); n /= 2; }
			for (long x = 3; x * x <= n && n > 1; x += 2)
				while (n % x == 0) { r.Add(x); n /= x; }
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

			var i = r.Count - 1;
			if (r[i] * r[i] == n) --i;
			for (; i >= 0; --i)
				r.Add(n / r[i]);
			return r.ToArray();
		}

		/// <summary>
		/// 指定された自然数が素数かどうかを判定します。O(√n)
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>素数である場合に限り true。</returns>
		public static bool IsPrime(long n)
		{
			// 候補 x を 2 または奇数に限定することで高速化します。
			if (n < 2 || n > 2 && n % 2 == 0) return false;
			for (long x = 3; x * x <= n; x += 2)
				if (n % x == 0) return false;
			return true;
		}

		/// <summary>
		/// 指定された自然数以下の素数をすべて求めます。O(n)?
		/// </summary>
		/// <param name="n">自然数。</param>
		/// <returns>素数のコレクション。</returns>
		public static long[] GetPrimes(long n)
		{
			// 候補 x を奇数に限定することで高速化します。
			var r = new List<long>();
			var b = new bool[n + 1];
			if (n > 1) r.Add(2);

			for (long p = 3; p <= n; p += 2)
				if (!b[p])
				{
					r.Add(p);
					for (long x = p * p; x <= n; x += 2 * p)
						b[x] = true;
				}

			return r.ToArray();
		}

		/// <summary>
		/// 指定された範囲内の素数をすべて求めます。O(√M)? or O(M - m)?
		/// </summary>
		/// <param name="m">最小値。</param>
		/// <param name="M">最大値。</param>
		/// <returns>素数のコレクション。</returns>
		public static long[] GetPrimes(long m, long M)
		{
			// n が大きい場合、誤差が生じる可能性があります。
			var rn = (int)Math.Ceiling(Math.Sqrt(M));

			var b1 = new bool[rn + 1];
			var b2 = new bool[M - m + 1];
			if (m == 1) b2[0] = true;

			for (long p = 2; p <= rn; ++p)
				if (!b1[p])
				{
					for (var x = p * p; x <= rn; x += p) b1[x] = true;
					for (var x = Math.Max(p, (m + p - 1) / p) * p; x <= M; x += p) b2[x - m] = true;
				}

			var r = new List<long>();
			for (int x = 0; x < b2.Length; ++x) if (!b2[x]) r.Add(m + x);
			return r.ToArray();
		}
	}
}
