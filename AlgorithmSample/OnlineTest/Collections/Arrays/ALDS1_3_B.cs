using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Arrays.ArrayQueue201;

// Queue
namespace OnlineTest.Collections.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_B
	class ALDS1_3_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qt) = Read2();
			var sb = new StringBuilder();

			var q = new ArrayQueue<(string, int)>();
			while (n-- > 0)
			{
				var p = Console.ReadLine().Split();
				q.Add((p[0], int.Parse(p[1])));
			}
			var t = 0;

			while (q.Count > 0)
			{
				var (name, time) = q.Pop();
				if (time <= qt)
				{
					sb.Append($"{name} {t += time}").AppendLine();
				}
				else
				{
					t += qt;
					q.Add((name, time - qt));
				}
			}
			Console.Write(sb);
		}
	}
}
