using System;

namespace AlgorithmLib.Maths
{
	public static class Euclidean
	{
		public static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
		public static int Lcm(int x, int y) => x / Gcd(x, y) * y;

		public static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
		public static long Lcm(long x, long y) => x / Gcd(x, y) * y;

		// ax + by = 1 の解
		// 前提: a と b は互いに素。
		// ax + by = GCD(a, b) の解を求める場合、予め GCD(a, b) で割ってからこの関数を利用します。
		public static (long x, long y) ExtendedEuclid(long a, long b)
		{
			if (b == 0) throw new ArgumentOutOfRangeException(nameof(b));
			if (b == 1) return (1, 1 - a);

			var q = Math.DivRem(a, b, out var r);
			var (x, y) = ExtendedEuclid(b, r);
			return (y, x - q * y);
		}
	}
}
