using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF411;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc333/tasks/abc333_d
	class ABC333_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

			var uf = new UnionFind(n + 1);

			foreach (var (u, v) in es)
			{
				if (u != 1)
					uf.Union(u, v);
			}
			return n - uf.GetSetInfoes().Max(g => g.Size);
		}
	}
}
