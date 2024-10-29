using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF401;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc334/tasks/abc334_e
	class ABC334_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (h, w) = Read2();
			var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

			var n = h * w;
			var p = s0.SelectMany(t => t.Select(c => c == '#')).ToArray();

			var uf = new UnionFind(n);

			for (int i = 0; i < h; i++)
				for (int j = 1; j < w; j++)
				{
					var v = w * i + j;
					if (p[v] && p[v - 1]) uf.Union(v, v - 1);
				}

			for (int j = 0; j < w; j++)
				for (int i = 1; i < h; i++)
				{
					var v = w * i + j;
					if (p[v] && p[v - w]) uf.Union(v, v - w);
				}

			var redsCount = 0;
			var sum = 0L;
			var nexts = new List<int>();

			for (int v = 0; v < n; v++)
			{
				if (p[v]) continue;
				redsCount++;

				var i = v / w;
				var j = v % w;

				nexts.Clear();
				if (i > 0 && p[v - w]) nexts.Add(v - w);
				if (i + 1 < h && p[v + w]) nexts.Add(v + w);
				if (j > 0 && p[v - 1]) nexts.Add(v - 1);
				if (j + 1 < w && p[v + 1]) nexts.Add(v + 1);

				sum += 1 - nexts.Select(uf.Find).Distinct().Count() + M;
			}

			sum %= M;
			return (uf.SetsCount - redsCount + sum * MInv(redsCount)) % M;
		}

		const long M = 998244353;
		// 0^0 は未定義
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);
	}
}
