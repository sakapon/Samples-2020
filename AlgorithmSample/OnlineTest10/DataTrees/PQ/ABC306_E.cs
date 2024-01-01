using AlgorithmLib10.DataTrees.PQ202;

namespace OnlineTest10.DataTrees.PQ
{
	// Test: https://atcoder.jp/contests/abc306/tasks/abc306_e
	class ABC306_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k, qc) = Read3();
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var a = new int[n];
			var lower = new RemovableListHeapQueue<int>(a[k..], descending: true);
			var upper = new RemovableListHeapQueue<int>(a[..k]);

			var s = 0L;
			var r = new List<long>();

			foreach (var (x, y) in qs)
			{
				var y0 = a[x - 1];
				a[x - 1] = y;

				if (y0 >= upper.First)
				{
					if (y >= upper.First)
					{
						upper.Remove(y0);
						upper.Push(y);
						s += y - y0;
					}
					else
					{
						upper.Remove(y0);
						lower.Push(y);
						upper.Push(lower.Pop());
						s += upper.First - y0;
					}
				}
				else
				{
					if (y >= upper.First)
					{
						s += y - upper.First;
						lower.Push(upper.Pop());
						upper.Push(y);
						lower.Remove(y0);
					}
					else
					{
						lower.Remove(y0);
						lower.Push(y);
					}
				}

				r.Add(s);
			}
			return string.Join("\n", r);
		}
	}
}
