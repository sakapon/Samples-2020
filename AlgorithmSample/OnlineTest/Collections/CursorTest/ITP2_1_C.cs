using System;
using System.Collections.Generic;
using AlgorithmLab.Collections;

namespace OnlineTest.Collections.CursorTest
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_C
	class ITP2_1_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main()
		{
			var qc = Read()[0];

			var l = new LinkedList<int>();
			var c = l.CreateCursor();

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					c.Insert(q[1]);
				}
				else if (q[0] == 1)
				{
					c.Move(q[1]);
				}
				else
				{
					c.Remove();
				}
			}
			Console.WriteLine(string.Join("\n", l));
		}
	}
}
