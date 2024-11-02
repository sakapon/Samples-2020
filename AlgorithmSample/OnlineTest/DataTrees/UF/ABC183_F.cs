using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF511;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc183/tasks/abc183_f
	class ABC183_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var c = Read().Prepend(0).ToArray();
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var r = new List<int>();
			var iv = Array.ConvertAll(c, y => new Dictionary<int, int> { { y, 1 } });
			var uf = new UnionFind<Dictionary<int, int>>(n + 1, Merge, iv);

			foreach (var (t, x, y) in qs)
			{
				if (t == 1)
				{
					uf.Union(x, y);
				}
				else
				{
					r.Add(uf.GetValue(x).GetValueOrDefault(y));
				}
			}
			return string.Join("\n", r);
		}

		public static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
		{
			if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
			foreach (var (k, v) in d2)
				if (d1.ContainsKey(k)) d1[k] += v;
				else d1[k] = v;
			return d1;
		}
	}
}
