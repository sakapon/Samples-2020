using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF451;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc120/tasks/abc120_d
	class ABC120_D_U
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var r = new List<long>();
			var s = 0L;
			var uf = new UnionFind(n + 1);
			var u = new bool[m];

			for (int j = m - 1; j >= 0; j--)
			{
				var (a, b) = es[j];
				u[j] = uf.Union(a, b);
			}

			for (int j = 0; j < m; j++)
			{
				if (u[j])
				{
					uf.Undo(out var a, out var b);
					s += (long)uf.GetSize(a) * uf.GetSize(b);
				}
				r.Add(s);
			}
			return string.Join("\n", r);
		}
	}
}
