using AlgorithmLib10.DataTrees.PQ.ListHeapQueueMap202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc323/tasks/abc323_d
	class ABC323_D
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2L());

			var r = 0;
			var map = new ListHeapQueueMap<long, long>(ps);

			while (map.Count > 0)
			{
				var (s, c) = map.Pop();
				while (map.Count > 0 && map.First.key == s)
					c += map.Pop().value;

				if ((c & 1) != 0) r++;
				s <<= 1;
				c >>= 1;
				if (c != 0) map.Push(s, c);
			}
			return r;
		}
	}
}
