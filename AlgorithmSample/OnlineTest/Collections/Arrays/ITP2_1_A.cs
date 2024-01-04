using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays201;

// ArrayList (ArrayStack ArrayDeque)
namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_A
	class ITP2_1_A
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main()
		{
			var qc = Read()[0];
			var sb = new StringBuilder();

			var l = new ArrayList<int>();

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					l.Add(q[1]);
				}
				else if (q[0] == 1)
				{
					sb.Append(l[q[1]]).AppendLine();
				}
				else
				{
					l.Pop();
				}
			}
			Console.Write(sb);
		}
	}
}
