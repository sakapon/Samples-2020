using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays.ArrayStack201;

// Stack (ArrayList Deque)
namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_A
	class ITP2_2_A
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qc) = Read2();
			var sb = new StringBuilder();

			var qs = Array.ConvertAll(new bool[n], _ => new ArrayStack<int>());

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					qs[q[1]].Add(q[2]);
				}
				else if (q[0] == 1)
				{
					if (qs[q[1]].Count > 0) sb.Append(qs[q[1]].First).AppendLine();
				}
				else
				{
					if (qs[q[1]].Count > 0) qs[q[1]].Pop();
				}
			}
			Console.Write(sb);
		}
	}
}
