using System;
using System.Collections.Generic;
using AlgorithmLab.Collections.ListHash201;

namespace OnlineTest.Collections.Hashing
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/8/ITP2_8_B
	class ITP2_8_B
	{
		static void Main()
		{
			var qc = int.Parse(Console.ReadLine());

			var map = new ListHashMap<string, string>(1 << 20);

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			while (qc-- > 0)
			{
				var q = Console.ReadLine().Split();

				if (q[0][0] == '0')
				{
					map[q[1]] = q[2];
				}
				else if (q[0][0] == '1')
				{
					Console.WriteLine(map.TryGetValue(q[1], out var v) ? v : "0");
				}
				else
				{
					map.Remove(q[1]);
				}
			}
			Console.Out.Flush();
		}
	}
}
