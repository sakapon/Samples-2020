using System;
using AlgorithmLab.IO;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/abc399/tasks/abc399_d
	class ABC399_D
	{
		static AsciiBlockReader cin = new AsciiBlockReader(Console.OpenStandardInput());
		static AsciiBlockWriter cout = new AsciiBlockWriter(Console.OpenStandardOutput());
		static void Main()
		{
			cin.Read(out int t);
			while (t-- > 0) Solve();
			cout.Flush();
		}

		static void Solve()
		{
			cin.Read(out int n);
			cin.Read(2 * n, out int[] a);

			var r = 0;
			var p = new int[n + 1];
			Array.Fill(p, -2);

			for (int i = 0; i < 2 * n - 1; i++)
			{
				var (u, v) = (a[i], a[i + 1]);
				var d = p[v] - p[u];

				if (d == 1)
				{
					if (i - p[v] >= 1)
						r++;
				}
				else if (d == -1)
				{
					if (i - p[u] > 1)
						r++;
				}

				p[u] = i;
			}
			cout.Write(r);
		}
	}
}
