using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF511;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc372/tasks/abc372_e
	class ABC372_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var r = new List<int>();
			var values = Enumerable.Range(0, n + 1).Select(i => new List<int> { i }).ToArray();
			var uf = new UnionFind<List<int>>(n + 1, (a, b) =>
			{
				a.AddRange(b);
				a.Sort();
				a.Reverse();
				while (a.Count > 10) a.RemoveAt(a.Count - 1);
				return a;
			}, values);

			foreach (var (t, u, v) in qs)
			{
				if (t == 1)
				{
					uf.Union(u, v);
				}
				else
				{
					var l = uf.GetValue(u);
					r.Add(l.Count >= v ? l[v - 1] : -1);
				}
			}
			return string.Join("\n", r);
		}
	}
}
