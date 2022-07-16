using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.DataTrees.PQ.HeapQueue201;

namespace OnlineTest.DataTrees.PQ
{
	// AOJ 向けに変更するコード
	//var t = a[i]; a[i] = a[i >> 1]; a[i >> 1] = t;
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
	class ALDS1_9_C
	{
		static void Main()
		{
			var sb = new StringBuilder();

			var asc = Comparer<int>.Default;
			var desc = Comparer<int>.Create((x, y) => asc.Compare(y, x));
			var q = new HeapQueue<int>(desc);

			string s;
			while ((s = Console.ReadLine())[2] != 'd')
			{
				if (s[0] == 'i')
				{
					q.Add(int.Parse(s.Substring(7)));
				}
				else
				{
					sb.Append(q.Pop()).AppendLine();
				}
			}
			Console.Write(sb);
		}
	}
}
