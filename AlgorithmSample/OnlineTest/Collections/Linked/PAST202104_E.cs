using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.Collections.Linked.LinkedDeque303;

// LinkedDeque
namespace OnlineTest.Collections.Linked
{
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_e
	class PAST202104_E
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();
			var sb = new StringBuilder();

			var dq = new LinkedDeque<int>();
			var dc = dq.CreateCursor();

			for (int i = 0; i < n; i++)
			{
				var c = s[i];

				if (c == 'L')
				{
					dc.JumpToFirst();
					dc.Add(i + 1);
				}
				else if (c == 'R')
				{
					dc.JumpToEnd();
					dc.Add(i + 1);
				}
				else if (c < 'D')
				{
					var k = c - 'A';
					if (k < dq.Count)
					{
						dc.Index = k;
						sb.Append(dc.Pop()).AppendLine();
					}
					else sb.AppendLine("ERROR");
				}
				else
				{
					var k = c - 'D';
					if (k < dq.Count)
					{
						dc.Index = dq.Count - 1 - k;
						sb.Append(dc.Pop()).AppendLine();
					}
					else sb.AppendLine("ERROR");
				}
			}
			Console.Write(sb);
		}
	}
}
