using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/9/ALDS1_9_C
// 0-based List
namespace AlgorithmLab.DataTrees.PQ.IntListHeap100
{
	public class IntListHeap : List<int>
	{
		public int First => this[0];

		void Swap(int i, int j) => (this[i], this[j]) = (this[j], this[i]);

		public new void Add(int item)
		{
			base.Add(item);

			// up-heap
			for (int i = Count - 1, j; i > 0 && this[j = (i - 1) / 2] > this[i]; Swap(i, i = j)) ;
		}

		public int Pop()
		{
			var item = this[0];
			this[0] = this[Count - 1];
			RemoveAt(Count - 1);

			// down-heap
			for (int i = 0, j; (j = 2 * i + 1) < Count;)
			{
				if (j + 1 < Count && this[j] > this[j + 1]) j++;
				if (this[i] > this[j]) Swap(i, i = j); else break;
			}
			return item;
		}
	}
}
