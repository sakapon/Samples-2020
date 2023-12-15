using AlgorithmLib10.Collections.MexMultiSet102;

namespace OnlineTest10.Collections.Mex
{
	// Test: https://atcoder.jp/contests/abc330/tasks/abc330_e
	class ABC330_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var a = Read();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			var set = new MexMultiSet(n, a);
			return string.Join("\n", qs.Select(q => Query(q[0] - 1, q[1])));

			int Query(int i, int x)
			{
				set.Remove(a[i]);
				a[i] = x;
				set.Add(a[i]);
				return set.Mex;
			}
		}
	}
}
