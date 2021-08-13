using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.Collections
{
	// キーが重複しない (すべての値の順序が異なる) 場合に使えます。
	// Peek: Min
	// Push: Add
	public class DistinctPriorityQueue<T> : SortedSet<T>
	{
		public DistinctPriorityQueue() { }
		public DistinctPriorityQueue(IComparer<T> comparer) : base(comparer) { }

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			var item = Min;
			Remove(item);
			return item;
		}
	}

	// キーと値が一対一に対応する場合に使えます。値の重複は可能です。
	// Count プロパティの値は正確ではありません。
	public class BstPriorityQueue<T> : SortedDictionary<T, int>
	{
		public BstPriorityQueue() { }
		public BstPriorityQueue(IComparer<T> comparer) : base(comparer) { }

		public T Peek()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			return this.First().Key;
		}

		public void Push(T item)
		{
			TryGetValue(item, out var count);
			this[item] = count + 1;
		}

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			var (item, count) = this.First();
			if (count == 1) Remove(item);
			else this[item] = count - 1;
			return item;
		}
	}
}
