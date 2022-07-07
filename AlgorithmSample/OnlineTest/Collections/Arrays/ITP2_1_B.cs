using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays.ArrayDeque201;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
	class ITP2_1_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main()
		{
			var qc = Read()[0];
			var sb = new StringBuilder();

			var dq = new ArrayDeque<int>();

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					if (q[1] == 0) dq.AddFirst(q[2]);
					else dq.AddLast(q[2]);
				}
				else if (q[0] == 1)
				{
					sb.Append(dq[q[1]]).AppendLine();
				}
				else
				{
					if (q[1] == 0) dq.PopFirst();
					else dq.PopLast();
				}
			}
			Console.Write(sb);
		}
	}
}
