using AlgorithmLib10.DataTrees.PQ.ListHeapQueue202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc137/tasks/abc137_d
	class ABC137_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var ps = Array.ConvertAll(new bool[n], _ => Read()).ToLookup(p => p[0], p => p[1]);

			var q = new ListHeapQueue<int>(null, true);

			var r = 0;
			for (int i = 1; i <= m; i++)
			{
				foreach (var b in ps[i]) q.Push(b);
				if (q.Count > 0) r += q.Pop();
			}
			return r;
		}
	}
}
