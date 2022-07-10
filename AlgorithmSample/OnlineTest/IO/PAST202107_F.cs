using System;
using System.Collections.Generic;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/past202107-open/tasks/past202107_f
	class PAST202107_F
	{
		static AsciiIO io = new AsciiIO();
		static void Main() => io.Write(Solve());
		static object Solve()
		{
			var n = io.Int();
			var ps = io.IntMatrix(n, 3);

			var u = new bool[100000 + 1, 24];

			foreach (var p in ps)
			{
				var (d, s, t) = (p[0], p[1], p[2]);
				for (int i = s; i < t; i++)
				{
					if (u[d, i]) return true;
					u[d, i] = true;
				}
			}
			return false;
		}
	}
}
