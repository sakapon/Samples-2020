using AlgorithmLib10.DataTrees.PQ202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc281/tasks/abc281_e
	class ABC281_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, k) = Read3();
			var a = Read();

			var am = a[..m];
			Array.Sort(am);

			var lower = new RemovableListHeapQueue<int>(am[..k], descending: true);
			var upper = new RemovableListHeapQueue<int>(am[k..]);

			var s = am[..k].Sum(x => (long)x);
			var r = new List<long> { s };

			for (int i = 0; i + m < n; i++)
			{
				var x0 = a[i];
				var x = a[i + m];

				if (x0 <= lower.First)
				{
					if (x <= lower.First)
					{
						lower.Remove(x0);
						lower.Push(x);
						s += x - x0;
					}
					else
					{
						lower.Remove(x0);
						upper.Push(x);
						lower.Push(upper.Pop());
						s += lower.First - x0;
					}
				}
				else
				{
					if (x <= lower.First)
					{
						upper.Remove(x0);
						lower.Push(x);
						upper.Push(lower.Pop());
						s += x - upper.First;
					}
					else
					{
						upper.Remove(x0);
						upper.Push(x);
					}
				}

				r.Add(s);
			}
			return string.Join(" ", r);
		}
	}
}
