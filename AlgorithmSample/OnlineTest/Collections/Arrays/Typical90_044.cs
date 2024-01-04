using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays201;

// ArrayDeque
namespace OnlineTest.Collections.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
	class Typical90_044
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main()
		{
			var qc = Read()[1];
			var a = Read();
			var sb = new StringBuilder();

			var dq = new ArrayDeque<int>(a);

			while (qc-- > 0)
			{
				var q = Read();
				var x = q[1] - 1;
				var y = q[2] - 1;

				if (q[0] == 1)
				{
					(dq[x], dq[y]) = (dq[y], dq[x]);
				}
				else if (q[0] == 2)
				{
					dq.AddFirst(dq.PopLast());
				}
				else
				{
					sb.Append(dq[x]).AppendLine();
				}
			}
			Console.Write(sb);
		}
	}
}
