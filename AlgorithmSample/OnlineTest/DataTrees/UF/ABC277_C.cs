using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF501;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc277/tasks/abc277_c
	class ABC277_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());

			// compression
			var set = new HashSet<int> { 1 };
			foreach (var (a, b) in es)
			{
				set.Add(a);
				set.Add(b);
			}
			var m = set.Count;
			var f = new int[m];
			set.CopyTo(f);
			Array.Sort(f);
			var map = Enumerable.Range(0, m).ToDictionary(i => f[i]);

			var uf = new UnionFind<int>(m, Math.Max, f);
			foreach (var (a, b) in es)
			{
				uf.Union(map[a], map[b]);
			}
			return uf[0];
		}
	}
}
