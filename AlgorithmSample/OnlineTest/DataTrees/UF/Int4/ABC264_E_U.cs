using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF451;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc264/tasks/abc264_e
	class ABC264_E_U
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

			foreach (var x in Enumerable.Range(1, ec).Except(qs).Concat(qs.Reverse()))
			{
				var (u, v) = es[x - 1];
				if (u > n) u = 0;
				if (v > n) v = 0;
				uf.Union(u, v);
			}

			for (int qi = 0; qi < qc; qi++)
			{
				uf.Undo(out var _, out var _);
				r.Add(uf.GetSize(0) - 1);
			}
			return string.Join("\n", r);
		}
	}
}
