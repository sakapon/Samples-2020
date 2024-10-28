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
			var s = s0.SelectMany(t => t).ToArray();

			var uf = new UnionFind(n);
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());

			for (int i = 0; i < h; i++)
				for (int j = 1; j < w; j++)
				{
					var v = w * i + j;
					if (s[v] == '#' && s[v - 1] == '#') uf.Union(v, v - 1);
					if (s[v] == '.' && s[v - 1] == '#') map[v].Add(v - 1);
					if (s[v] == '#' && s[v - 1] == '.') map[v - 1].Add(v);
				}

			for (int j = 0; j < w; j++)
				for (int i = 1; i < h; i++)
				{
					var v = w * i + j;
					if (s[v] == '#' && s[v - w] == '#') uf.Union(v, v - w);
					if (s[v] == '.' && s[v - w] == '#') map[v].Add(v - w);
					if (s[v] == '#' && s[v - w] == '.') map[v - w].Add(v);
				}

			var reds = Enumerable.Range(0, n).Where(v => s[v] == '.').ToArray();
			var sum = reds.Select(v => 1 - map[v].Select(uf.Find).Distinct().Count() + M).Aggregate((x, y) => (x + y) % M);
			return (uf.SetsCount - reds.Length + sum * MInv(reds.Length)) % M;
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
