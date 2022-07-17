using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.PQ.HeapQueue201;

namespace OnlineTest.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc141/tasks/abc141_d
	class ABC141_D
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var m = ReadL()[1];
			var a = ReadL();

			var q = new HeapQueue<long>(true, a);
			while (m-- > 0) q.Add(q.Pop() / 2);
			return q.GetRawItems().Sum();
		}
	}
}
