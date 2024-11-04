using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/arc114/tasks/arc114_b
	class ARC114_B
	{
		const long M = 998244353;
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var f = Read();

			var uf = new UnionFind(n + 1);
			for (int i = 1; i <= n; i++)
				uf.Union(i, f[i - 1]);

			var r = 1L;
			var c = uf.SetsCount - 1;
			while (c-- > 0)
				r = r * 2 % M;
			return (r - 1 + M) % M;
		}
	}
}
