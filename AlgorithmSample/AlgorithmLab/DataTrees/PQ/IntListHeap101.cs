using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// 1-based List
namespace AlgorithmLab.DataTrees.PQ.IntListHeap101
{
	public class IntListHeap
	{
		readonly List<int> l = new List<int>();
		public int Count => l.Count - 1;
		public int First => l[1];
		public IntListHeap() { Clear(); }

		public void Clear()
		{
			l.Clear();
			l.Add(0);
		}

		void Swap(int i, int j) => (l[i], l[j]) = (l[j], l[i]);

		public void Add(int item)
		{
			l.Add(item);

			// up-heap
			for (int i = l.Count - 1; i > 1 && l[i] < l[i >> 1]; i >>= 1)
			{
				Swap(i, i >> 1);
			}
		}

		public int Pop()
		{
			var item = l[1];
			l[1] = l[l.Count - 1];
			l.RemoveAt(l.Count - 1);

			// down-heap
			for (int i = 2; i < l.Count; i <<= 1)
			{
				if (i + 1 < l.Count && l[i] > l[i + 1]) ++i;
				if (l[i] >= l[i >> 1]) break;
				Swap(i, i >> 1);
			}
			return item;
		}
	}
}
