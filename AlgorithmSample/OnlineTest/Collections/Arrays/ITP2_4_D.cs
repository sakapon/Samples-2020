using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Collections.Arrays202;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/4/ITP2_4_D
	class ITP2_4_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			Console.ReadLine();
			var a = Read();

			var set = new ArrayHashSet<int>(1 << 19);
			foreach (var x in a) set.Add(x);
			a = set.ToArray();
			Array.Sort(a);
			return string.Join(" ", a);
		}
	}
}
