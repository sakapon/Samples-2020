using System;
using System.Collections.Generic;

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
			var p = new int[n + 1];
			Array.Fill(p, -2);

			for (int i = 0; i < 2 * n - 1; i++)
			{
				var (u, v) = (a[i], a[i + 1]);
				var d = p[v] - p[u];

				if (d == 1)
				{
					if (i - p[v] >= 1)
						r++;
				}
				else if (d == -1)
				{
					if (i - p[u] > 1)
						r++;
				}

				p[u] = i;
			}
			return r;
		}
	}
}
