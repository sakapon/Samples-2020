using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// 容量を固定します。
namespace AlgorithmLab.DataTrees.PQ.HeapQueue101
{
	public class HeapQueue<T>
	{
		T[] a;
		int n = 1;
		readonly IComparer<T> c;

		public HeapQueue(int capacity, IComparer<T> comparer = null)
		{
			a = new T[capacity];
			c = comparer ?? Comparer<T>.Default;
		}

		public int Count => n - 1;
		public T First => a[1];

		public void Clear() => n = 1;

		public void Add(T item)
		{
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
	}
}
