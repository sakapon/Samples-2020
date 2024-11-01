using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF411;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc226/tasks/abc226_e
	class ABC226_E2
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			if (n != m) return 0;

			var ecs = new int[n + 1];
			var uf = new UnionFind(n + 1);
			uf.United += (v, nv) =>
			{
				ecs[v] += ecs[nv];
				ecs[nv] = 0;
			};

			foreach (var (u, v) in es)
			{
				uf.Union(u, v);
				ecs[uf.Find(v).Item]++;
			}

			var r = 1L;
			for (int v = 1; v <= n; v++)
			{
				if (ecs[v] == 0) continue;
				if (uf.GetSize(v) != ecs[v]) return 0;
				r = r * 2 % M;
			}
			return r;
		}
	}
}
