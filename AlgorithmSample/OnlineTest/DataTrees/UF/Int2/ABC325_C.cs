using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc325/tasks/abc325_c
	class ABC325_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var s = s0.SelectMany(t => t).ToArray();

			var uf = new UnionFind(n);

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					var v = w * i + j;

					if (j > 0)
					{
						if (s[v] == '#' && s[v - 1] == '#') uf.Union(v, v - 1);
					}

					if (i > 0)
					{
						if (s[v] == '#' && s[v - w] == '#') uf.Union(v, v - w);
					}

					if (i > 0 && j > 0)
					{
						if (s[v] == '#' && s[v - w - 1] == '#') uf.Union(v, v - w - 1);
						if (s[v - 1] == '#' && s[v - w] == '#') uf.Union(v - 1, v - w);
					}
				}

			var r = 0;
			var u = new bool[n];
			for (int v = 0; v < n; v++)
			{
				var rv = uf.Find(v);
				if (u[rv]) continue;
				u[rv] = true;
				if (s[rv] == '#') r++;
			}
			return r;
		}
	}
}
