using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Collections.Arrays201;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://atcoder.jp/contests/abc073/tasks/abc073_c
	class ABC073_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var set = new ArrayHashSet<int>();
			foreach (var x in a)
				if (!set.Add(x)) set.Remove(x);
			return set.Count;
		}

		static object Solve2()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var map = new ArrayHashMap<int, int>();
			foreach (var x in a) map[x]++;
			return map.Count(p => p.value % 2 == 1);
		}
	}
}
