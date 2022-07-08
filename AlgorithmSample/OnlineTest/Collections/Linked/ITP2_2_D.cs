using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Linked.LinkedDeque302;

// LinkedDeque (LinkedQueue)
namespace OnlineTest.Collections.Linked
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_D
	class ITP2_2_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qc) = Read2();
			var sb = new StringBuilder();

			var qs = Array.ConvertAll(new bool[n], _ => new LinkedDeque<int>());

			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
				{
					qs[q[1]].AddLast(q[2]);
				}
				else if (q[0] == 1)
				{
					sb.AppendLine(string.Join(" ", qs[q[1]]));
				}
				else
				{
					qs[q[2]].ConcatLast(qs[q[1]]);
				}
			}
			Console.Write(sb);
		}
	}
}
