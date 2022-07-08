using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.Arrays.ArrayStack201;

// Stack
namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_A
	class ALDS1_3_A
	{
		static void Main()
		{
			var es = Console.ReadLine().Split();

			var s = new ArrayStack<int>();

			var actions = new Dictionary<string, Action>();
			actions["+"] = () => s.Add(s.Pop() + s.Pop());
			actions["-"] = () => s.Add(-s.Pop() + s.Pop());
			actions["*"] = () => s.Add(s.Pop() * s.Pop());

			foreach (var e in es)
			{
				if (actions.ContainsKey(e))
				{
					actions[e]();
				}
				else
				{
					s.Add(int.Parse(e));
				}
			}
			Console.WriteLine(s.Pop());
		}
	}
}
