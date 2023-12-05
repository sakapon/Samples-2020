using AlgorithmLib10.Collections.MexMultiSet102;

namespace OnlineTest10.Collections.Mex
{
	// Test: https://atcoder.jp/contests/abc194/tasks/abc194_e
	class ABC194_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();

			var set = new MexMultiSet(m, a[0..m]);
			var r = set.Mex;

			for (int i = 0; i + m < n; i++)
			{
				set.Remove(a[i]);
				set.Add(a[i + m]);
				r = Math.Min(r, set.Mex);
			}
			return r;
		}
	}
}
