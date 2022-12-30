using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_C
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// 0-based List
namespace AlgorithmLab.DataTrees.PQ.ListHeap100
{
	public class ListHeap<T> : List<T>
	{
		public IComparer<T> Comparer { get; }
		public T First => this[0];
		public ListHeap(IComparer<T> comparer = null) { Comparer = comparer ?? Comparer<T>.Default; }

		void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
		void UpHeap(int i) { for (int j; i > 0 && Comparer.Compare(this[j = (i - 1) / 2], this[i]) > 0; Swap(i, i = j)) ; }
		void DownHeap(int i)
		{
			for (int j; (j = 2 * i + 1) < Count;)
			{
				if (j + 1 < Count && Comparer.Compare(this[j], this[j + 1]) > 0) j++;
				if (Comparer.Compare(this[i], this[j]) > 0) Swap(i, i = j); else break;
			}
		}

		public new void Add(T item)
		{
			base.Add(item);
			UpHeap(Count - 1);
		}

		public T Pop()
		{
			var item = this[0];
			this[0] = this[Count - 1];
			RemoveAt(Count - 1);
			DownHeap(0);
			return item;
		}
	}
}
