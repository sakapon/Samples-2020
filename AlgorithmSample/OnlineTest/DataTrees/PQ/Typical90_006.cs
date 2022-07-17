using System;
using System.Collections.Generic;
using AlgorithmLab.DataTrees;
using AlgorithmLab.DataTrees.PQ.HeapQueue201;

namespace OnlineTest.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
	class Typical90_006
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2();
			var s = Console.ReadLine();

			var cs = new char[k];

			var comp = ComparerHelper<int>.Create(i => s[i], false, i => i, false);
			var q = new HeapQueue<int>(comp);
			for (int i = 0; i < n - k; i++)
			{
				q.Add(i);
			}

			var t = -1;
			for (int i = n - k; i < n; i++)
			{
				q.Add(i);

				while (true)
				{
					var j = q.Pop();
					if (j < t) continue;

					cs[i - n + k] = s[t = j];
					break;
				}
			}

			return new string(cs);
		}
	}
}
