using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc229/tasks/abc229_e
	class ABC229_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var map = es.ToLookup(e => e.u, e => e.v);

			var r = new List<int>();
			var uf = new UnionFind(n + 1);

			for (int v = n; v > 0; v--)
			{
				r.Add(uf.SetsCount - v - 1);

				foreach (var nv in map[v])
				{
					uf.Union(v, nv);
				}
			}

			r.Reverse();
			return string.Join("\n", r);
		}
	}
}
