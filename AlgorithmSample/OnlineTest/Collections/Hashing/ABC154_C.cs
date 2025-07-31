using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Collections.HashTables;

namespace OnlineTest.Collections.Hashing
{
	// Test: https://atcoder.jp/contests/abc154/tasks/abc154_c
	class ABC154_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve() ? "YES" : "NO");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var set = new ChainHashSet<int>(1 << 20);
			return a.All(set.Add);
		}
	}
}
