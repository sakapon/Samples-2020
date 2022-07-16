using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_C
namespace AlgorithmLab.DataTrees.PQ.HeapQueue201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class HeapQueue<T>
	{
		T[] a;
		int n = 1;
		readonly IComparer<T> c;

		public HeapQueue(IComparer<T> comparer = null, IEnumerable<T> items = null)
		{
			a = new T[4];
			c = comparer ?? Comparer<T>.Default;
			if (items != null) foreach (var item in items) Add(item);
		}

		public HeapQueue(bool descending, IEnumerable<T> items = null) : this(GetComparer(descending), items) { }
		static IComparer<T> GetComparer(bool descending)
		{
			var asc = Comparer<T>.Default;
			return descending ? Comparer<T>.Create((x, y) => asc.Compare(y, x)) : asc;
		}

		public int Count => n - 1;
		public IComparer<T> Comparer => c;
		public T First => a[1];

		public void Clear() => n = 1;

		public void Add(T item)
		{
			if (n == a.Length) Expand();
			a[n++] = item;

			for (int i = n - 1; i > 1 && c.Compare(a[i], a[i >> 1]) < 0; i >>= 1)
			{
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
		}

		public T Pop()
		{
			var item = a[1];
			a[1] = a[--n];

			for (int i = 2; i < n; i <<= 1)
			{
				if (i + 1 < n && c.Compare(a[i], a[i + 1]) > 0) ++i;
				if (c.Compare(a[i], a[i >> 1]) >= 0) break;
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
			return item;
		}

		void Expand()
		{
			Array.Resize(ref a, a.Length << 1);
		}

		public IEnumerable<T> GetRawItems() { for (var i = 1; i < n; ++i) yield return a[i]; }
	}
}
