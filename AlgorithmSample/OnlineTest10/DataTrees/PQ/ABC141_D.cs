using AlgorithmLib10.DataTrees.PQ.ListHeapQueue202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc141/tasks/abc141_d
	class ABC141_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var m = Read()[1];
			var a = Read();

			var q = new ListHeapQueue<int>(a, null, true);
			while (m-- > 0) q.Push(q.Pop() / 2);
			return q.Raw.Sum(v => (long)v);
		}
	}
}
