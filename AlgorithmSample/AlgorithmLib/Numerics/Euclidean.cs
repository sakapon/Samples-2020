using System;

namespace AlgorithmLib.Numerics
{
	public static class Euclidean
	{
		public static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
		public static int Lcm(int a, int b) => a / Gcd(a, b) * b;

		public static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
		public static long Lcm(long a, long b) => a / Gcd(a, b) * b;

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
