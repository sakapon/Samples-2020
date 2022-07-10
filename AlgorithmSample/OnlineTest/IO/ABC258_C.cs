using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc258/tasks/abc258_c
	class ABC258_C
	{
		static AsciiIO io = new AsciiIO();
		static void Main()
		{
			var n = io.Int();
			var qc = io.Int();
			var s = io.String();
			var sb = new StringBuilder();

			var fi = 0;

			while (qc-- > 0)
			{
				var q = io.Int(2);
				var x = q[1];
				if (q[0] == 1)
				{
					fi = (fi - x + n) % n;
				}
				else
				{
					sb.Append(s[(fi + x - 1) % n]).AppendLine();
				}
			}
			Console.Write(sb);
		}
	}
}
