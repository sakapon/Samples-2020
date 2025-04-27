using System;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc258/tasks/abc258_c
	class ABC258_C
	{
		static AsciiBlockReader cin = new AsciiBlockReader(Console.OpenStandardInput());
		static AsciiBlockWriter cout = new AsciiBlockWriter(Console.OpenStandardOutput());
		static void Main()
		{
			cin.Read(out int n);
			cin.Read(out int qc);
			cin.Read(out string s);

			var fi = 0;

			while (qc-- > 0)
			{
				cin.Read(out int t);
				cin.Read(out int x);
				if (t == 1)
				{
					fi = (fi - x + n) % n;
				}
				else
				{
					cout.Write(s[(fi + x - 1) % n]);
				}
			}
			cout.Flush();
		}
	}
}
