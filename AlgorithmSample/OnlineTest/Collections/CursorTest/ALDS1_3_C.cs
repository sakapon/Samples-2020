using System;
using System.Collections.Generic;
using AlgorithmLab.Collections;

// LinkedDeque
namespace OnlineTest.Collections.CursorTest
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_C
	class ALDS1_3_C
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());

			var l = new LinkedList<int>();
			var c = l.CreateCursor();

			var ac = new Action<string[]>[1 << 7];
			ac['n' + 6] = q =>
			{
				c.Repoint(0);
				c.Add(int.Parse(q[1]));
			};
			ac['e' + 6] = q =>
			{
				var x = int.Parse(q[1]);
				for (c.Repoint(0); !c.IsAtEnd; c.MoveNext())
					if (c.Item == x) { c.Remove(); break; }
			};
			ac['e' + 11] = q =>
			{
				c.Repoint(0);
				c.Remove();
			};
			ac['e' + 10] = q =>
			{
				c.Repoint(l.Count - 1);
				c.Remove();
			};

			while (n-- > 0)
			{
				var q = Console.ReadLine().Split();
				ac[q[0][1] + q[0].Length](q);
			}
			Console.WriteLine(string.Join(" ", l));
		}
	}
}
