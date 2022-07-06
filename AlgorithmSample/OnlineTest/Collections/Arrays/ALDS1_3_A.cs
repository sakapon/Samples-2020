using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.Arrays.ArrayStack201;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_A
	class ALDS1_3_A
	{
		static void Main()
		{
			var es = Console.ReadLine().Split();
			var s = new ArrayStack<int>();

			foreach (var e in es)
			{
				switch (e)
				{
					case "+":
						s.Push(s.Pop() + s.Pop());
						break;
					case "-":
						s.Push(-s.Pop() + s.Pop());
						break;
					case "*":
						s.Push(s.Pop() * s.Pop());
						break;
					default:
						s.Push(int.Parse(e));
						break;
				}
			}
			Console.WriteLine(s.Pop());
		}
	}
}
