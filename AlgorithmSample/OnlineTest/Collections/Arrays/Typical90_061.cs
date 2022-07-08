using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays.ArrayDeque201;

// ArrayDeque
namespace OnlineTest.Collections.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
	class Typical90_061
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

				if (q[0] == 1)
				{
					dq.AddFirst(q[1]);
				}
				else if (q[0] == 2)
				{
					dq.AddLast(q[1]);
				}
				else
				{
					sb.Append(dq[q[1] - 1]).AppendLine();
				}
			}
			Console.Write(sb);
		}
	}
}
