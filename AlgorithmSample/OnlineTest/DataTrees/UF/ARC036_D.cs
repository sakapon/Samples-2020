﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnionFind1 = AlgorithmLab.DataTrees.UF511.UnionFind<bool>;
using UnionFind2 = AlgorithmLab.DataTrees.UF602.UnionFind<bool>;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/arc036/tasks/arc036_d
	class ARC036_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());

			var r = new List<bool>();
			var uf1 = new UnionFind1(n + 1, (x, y) => x | y);
			var uf2 = new UnionFind2(n + 1, false, x => x, (x, y) => x ^ y);

			foreach (var (w, x, y, z) in qs)
			{
				if (w == 1)
				{
					uf1.Union(x, y);
					if (uf1.GetValue(x)) continue;

					if (!uf2.Union(x, y, z % 2 != 0) && !uf2.Verify(x, y, z % 2 != 0))
					{
						uf1.Find(x).Value = true;
					}
				}
				else
				{
					r.Add(uf1.AreSame(x, y) && (uf1.GetValue(x) || uf2.Verify(x, y, false)));
				}
			}
			return string.Join("\n", r.Select(b => b ? "YES" : "NO"));
		}
	}
}
