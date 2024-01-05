using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.Arrays301;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://atcoder.jp/contests/abc328/tasks/abc328_d
	class ABC328_D
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var s = Console.ReadLine();

			var q = new ArrayDeque<char>();

			foreach (var c in s)
			{
				if (c == 'C' && q.Count >= 2 && q[0] == 'B' && q[1] == 'A')
				{
					q.PopFirst();
					q.PopFirst();
				}
				else
				{
					q.AddFirst(c);
				}
			}

			var cs = q.ToArray();
			Array.Reverse(cs);
			return new string(cs);
		}
	}
}
