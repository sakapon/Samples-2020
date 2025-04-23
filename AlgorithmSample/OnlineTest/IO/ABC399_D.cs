using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc399/tasks/abc399_d
	class ABC399_D
	{
		static void Main()
		{
			IOHelper.In.Read(out int t);
			var r = Array.ConvertAll(new bool[t], _ => Solve());
			Console.WriteLine(string.Join("\n", r));
		}

		static int Solve()
		{
			IOHelper.In
				.Read(out int n)
				.ReadLine(out int[] a);

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
