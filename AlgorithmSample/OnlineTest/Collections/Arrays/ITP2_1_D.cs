using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays.ArrayList201;

namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_D
	class ITP2_1_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qc) = Read2();
			var sb = new StringBuilder();

			var ls = Array.ConvertAll(new bool[n], _ => new ArrayList<int>());

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					ls[q[1]].Add(q[2]);
				}
				else if (q[0] == 1)
				{
					sb.AppendLine(string.Join(" ", ls[q[1]]));
				}
				else
				{
					ls[q[1]].Clear();
				}
			}
			Console.Write(sb);
		}
	}
}
