using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.ListHash101;

namespace OnlineTest.Collections.Hashing
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_aa
	class Typical90_027
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());

			var set = new ListHashSet<string>(1 << 18);

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			for (int i = 1; i <= n; i++)
			{
				var s = Console.ReadLine();
				if (set.Add(s)) Console.WriteLine(i);
			}
			Console.Out.Flush();
		}
	}
}
