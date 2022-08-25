using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.Linked.DistinctLinkedList202;

namespace OnlineTest.Collections.Linked
{
	// Test: https://atcoder.jp/contests/abc237/tasks/abc237_d
	class ABC237_D
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();

			var l = new DistinctLinkedList(n + 1);
			l.AddFirst(0);

			for (int i = 1; i <= n; i++)
			{
				if (s[i - 1] == 'L')
				{
					l.AddBefore(i - 1, i);
				}
				else
				{
					l.AddAfter(i - 1, i);
				}
			}
			return string.Join(" ", l);
		}
	}
}
