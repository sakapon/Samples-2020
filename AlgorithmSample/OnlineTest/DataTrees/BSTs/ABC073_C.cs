using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.BSTs.BSTs203;

namespace OnlineTest.DataTrees.BSTs
{
	// Test: https://atcoder.jp/contests/abc073/tasks/abc073_c
	class ABC073_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var set = new Int32LcaTreeSet();
			foreach (var x in a)
				if (!set.Add(x)) set.Remove(x);
			return set.Count;
		}
	}
}
