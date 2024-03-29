﻿using System;
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
				c.JumpToFirst();
				c.Add(int.Parse(q[1]));
			};
			ac['e' + 6] = q =>
			{
				var x = int.Parse(q[1]);
				for (c.JumpToFirst(); !c.IsAtEnd; c.MoveNext())
					if (c.Item == x) { c.Pop(); break; }
			};
			ac['e' + 11] = q =>
			{
				c.JumpToFirst();
				c.Pop();
			};
			ac['e' + 10] = q =>
			{
				c.JumpToEnd();
				c.MovePrevious();
				c.Pop();
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
