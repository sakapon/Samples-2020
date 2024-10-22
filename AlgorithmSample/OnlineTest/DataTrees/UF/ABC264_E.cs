using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF511;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc264/tasks/abc264_e
	class ABC264_E
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
			var s = 0;

			var values = new (bool, int)[n + m + 1];
			Array.Fill(values, (false, 1), 0, n + 1);
			Array.Fill(values, (true, 0), n + 1, m);

			var uf = new UnionFind<(bool u, int c)>(n + m + 1, (a, b) =>
			{
				if (a.u != b.u) s += a.u ? b.c : a.c;
				return (a.u || b.u, a.c + b.c);
			}, values);

			foreach (var x in Enumerable.Range(1, ec).Except(qs))
			{
				var (u, v) = es[x - 1];
				uf.Union(u, v);
			}

			Array.Reverse(qs);
			foreach (var x in qs)
			{
				r.Add(s);
				var (u, v) = es[x - 1];
				uf.Union(u, v);
			}

			r.Reverse();
			return string.Join("\n", r);
		}
	}
}
