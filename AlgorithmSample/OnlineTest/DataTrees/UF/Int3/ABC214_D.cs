using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc214/tasks/abc214_d
	class ABC214_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int w) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read3());

			var r = 0L;
			var uf = new UnionFind(n + 1);

			foreach (var (u, v, w) in es.OrderBy(e => e.w))
			{
				r += (long)w * uf.GetSize(u) * uf.GetSize(v);
				uf.Union(u, v);
			}
			return r;
		}
	}
}
