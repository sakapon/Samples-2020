using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.SUF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc177/tasks/abc177_d
	class ABC177_D_S
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var g = new StaticUnionGraph(n + 1, es);
			var uf = g.Build();
			return uf.Sets.Max(set => set.Length);
		}
	}
}
