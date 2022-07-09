using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/past202109-open/tasks/past202109_c
	class PAST202109_C
	{
		static AsciiIO io = new AsciiIO();
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = io.Int();
			var x = io.Int();
			var a = io.Int(n);

			return a.Count(v => v == x);
		}
	}
}
