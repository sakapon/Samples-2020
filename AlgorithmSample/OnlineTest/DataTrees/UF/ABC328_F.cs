﻿using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF611;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/abc328/tasks/abc328_f
	class ABC328_F
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
			var uf = new UnionFind(n + 1);

			for (int i = 0; i < qc; i++)
			{
				var (a, b, d) = qs[i];

				if (uf.Union(b, a, d) || uf[a].Value - uf[b].Value == d)
				{
					r.Add(i + 1);
				}
			}
			return string.Join(" ", r);
		}
	}
}
