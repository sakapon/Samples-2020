using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF502;

// 402, 502
namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc277/tasks/abc277_c
	class ABC277_C2
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n], _ => Read2());

			var uf = new UnionFind<int, int>(Math.Max);
			uf.Add(1, 1);
			foreach (var (a, b) in es)
			{
				uf.Add(a, a);
				uf.Add(b, b);
				uf.Union(a, b);
			}
			return uf[1];
		}
	}
}
