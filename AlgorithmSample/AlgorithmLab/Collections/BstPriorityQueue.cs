using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections
{
	// キーが重複しない (すべての値の順序が異なる) 場合に使えます。
	// First: Min
	// Push: Add
	public class DistinctPriorityQueue<T> : SortedSet<T>
	{
		public DistinctPriorityQueue() { }
		public DistinctPriorityQueue(IComparer<T> comparer) : base(comparer) { }

		public T Pop()
		{
			var r = Min;
			Remove(r);
			return r;
		}
	}
}
