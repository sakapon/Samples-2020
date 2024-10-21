using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc247/tasks/abc247_f
	class ABC247_F
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read();
			var q = Read();

			var uf = new UnionFind(n + 1);
			for (int i = 0; i < n; i++)
				uf.Union(p[i], q[i]);

			var f = new long[n + 1];
			f[0] = 2;
			f[1] = 1;
			for (int i = 2; i <= n; i++)
				f[i] = (f[i - 1] + f[i - 2]) % M;

			return uf.ToSets().Select(g => f[g.Count()]).Aggregate((x, y) => x * y % M);
		}
	}
}
