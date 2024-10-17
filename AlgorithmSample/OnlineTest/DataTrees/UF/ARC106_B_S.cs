using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.SUF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/arc106/tasks/arc106_b
	class ARC106_B_S
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var (n, m) = Read2();
			var a = ReadL();
			var b = ReadL();
			var es = Array.ConvertAll(new bool[m], _ => Read2());

			var g = new StaticUnionGraph(n + 1, es);
			var uf = g.Build();
			return uf.Sets[1..].All(set => set.Sum(v => a[v - 1]) == set.Sum(v => b[v - 1]));
		}
	}
}
