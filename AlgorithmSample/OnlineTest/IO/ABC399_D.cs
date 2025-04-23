using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc399/tasks/abc399_d
	class ABC399_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main()
		{
			var t = int.Parse(Console.ReadLine());
			var r = Array.ConvertAll(new bool[t], _ => Solve());
			Console.WriteLine(string.Join("\n", r));
		}

		static int Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var r = 0;
			var d = new Dictionary<(int, int), int>();

			for (int i = 1; i < 2 * n; i++)
			{
				var (u, v) = (a[i - 1], a[i]);
				if (u > v) (u, v) = (v, u);
				if (!d.ContainsKey((u, v)))
					d[(u, v)] = i;
				else if (i - d[(u, v)] > 2)
					r++;
			}
			for (int i = 3; i < 2 * n; i++)
			{
				if (a[i - 3] == a[i - 1] && a[i - 2] == a[i])
					r++;
			}
			return r;
		}
	}
}
