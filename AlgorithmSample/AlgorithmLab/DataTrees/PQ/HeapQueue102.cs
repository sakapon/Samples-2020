using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// 容量を固定します。
// 0-based array
namespace AlgorithmLab.DataTrees.PQ.HeapQueue102
{
	public class HeapQueue<T>
	{
		T[] a;
		int n;
		readonly IComparer<T> comp;

		public HeapQueue(int capacity, IComparer<T> comparer = null)
		{
			a = new T[capacity];
			comp = comparer ?? Comparer<T>.Default;
		}

		public int Count => n;
		public IComparer<T> Comparer => comp;
		public T First => a[0];

		public void Clear() => n = 0;

		public void Add(T item)
		{
			a[n++] = item;

			for (int c = n - 1, p; c > 0 && comp.Compare(a[c], a[p = (c - 1) >> 1]) < 0; c = p)
			{
				(a[c], a[p]) = (a[p], a[c]);
			}
		}

		public T Pop()
		{
			var item = a[0];
			a[0] = a[--n];

			for (int c, p = 0; (c = p << 1 | 1) < n; p = c)
			{
				if (c + 1 < n && comp.Compare(a[c], a[c + 1]) > 0) ++c;
				if (comp.Compare(a[c], a[p]) >= 0) break;
				(a[c], a[p]) = (a[p], a[c]);
			}
			return item;
		}
	}
}
