using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc255/tasks/abc255_e
	class ABC255_E
	{
		static AsciiIO io = new AsciiIO();
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = io.Int();
			var m = io.Int();
			var s = io.Long(n);
			var x = io.Long(m);

			var a = new long[n];
			for (int i = 1; i < n; i++)
				a[i] = s[i - 1] - a[i - 1];

			var map = new Dictionary<long, int>();

			foreach (var ln in x)
			{
				for (int i = 0; i < n; i++)
				{
					var d = ln - a[i];
					if (i % 2 == 1) d *= -1;

					if (map.ContainsKey(d)) map[d]++;
					else map[d] = 1;
				}
			}
			return map.Values.Max();
		}
	}
}
