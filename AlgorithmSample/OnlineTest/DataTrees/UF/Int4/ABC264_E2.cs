using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc264/tasks/abc264_e
	class ABC264_E2
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, ec) = Read3();
			var es = Array.ConvertAll(new bool[ec], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

			var r = new List<int>();
			var uf = new UnionFind(n + 1);

			foreach (var x in Enumerable.Range(1, ec).Except(qs))
			{
				var (u, v) = es[x - 1];
				if (u > n) u = 0;
				if (v > n) v = 0;
				uf.Union(u, v);
			}

			Array.Reverse(qs);
			foreach (var x in qs)
			{
				r.Add(uf.GetSize(0) - 1);

				var (u, v) = es[x - 1];
				if (u > n) u = 0;
				if (v > n) v = 0;
				uf.Union(u, v);
			}

			r.Reverse();
			return string.Join("\n", r);
		}
	}
}
