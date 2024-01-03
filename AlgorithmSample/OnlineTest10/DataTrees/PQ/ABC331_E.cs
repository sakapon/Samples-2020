using AlgorithmLib10.DataTrees.PQ202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc331/tasks/abc331_e
	class ABC331_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int c, int d) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, l) = Read3();
			var a = Read();
			var b = Read();
			var ps = Array.ConvertAll(new bool[l], _ => Read2());

			var r = 0;
			var bset = new ListHeapMultiSet<int>(b, null, true);
			var bads = ps.ToLookup(p => p.c, p => p.d);

			for (int i = 1; i <= n; i++)
			{
				foreach (var j in bads[i]) bset.Remove(b[j - 1]);
				if (bset.Count > 0) r = Math.Max(r, a[i - 1] + bset.First);
				foreach (var j in bads[i]) bset.Add(b[j - 1]);
			}
			return r;
		}
	}
}
