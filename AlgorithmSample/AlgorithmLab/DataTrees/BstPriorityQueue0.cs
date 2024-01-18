using System;
using System.Collections.Generic;
using System.Linq;
using Col = AlgorithmLab.Collections.Arrays001;

namespace AlgorithmLab.DataTrees
{
	// 要素が重複しない (すべての値の順序が異なる) 場合に利用できます。
	// Peek: Min
	// Push: Add
	public class DistinctPriorityQueue0<T> : SortedSet<T>
	{
		public DistinctPriorityQueue0(IComparer<T> comparer = null) : base(comparer ?? Comparer<T>.Default) { }

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			var item = Min;
			Remove(item);
			return item;
		}
	}

	// 要素が重複する場合も利用できます (一般的な優先度付きキュー)。
	// Count プロパティの値は正確ではありません。
	public class BstPriorityQueue0<T> : SortedDictionary<T, int>
	{
		public BstPriorityQueue0(IComparer<T> comparer = null) : base(comparer ?? Comparer<T>.Default) { }

		public T Peek()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			return this.First().Key;
		}

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			var (item, count) = this.First();
			if (count == 1) Remove(item);
			else this[item] = count - 1;
			return item;
		}

		public void Push(T item)
		{
			TryGetValue(item, out var count);
			this[item] = count + 1;
		}
	}

	// 要素に対して優先度を表すキーを指定する場合に利用します。
	// Count プロパティの値は正確ではありません。
	public class KeyedPriorityQueue0<T, TKey> : SortedDictionary<TKey, Col.Queue<T>>
	{
		Func<T, TKey> keySelector;

		public KeyedPriorityQueue0(Func<T, TKey> keySelector, IComparer<TKey> comparer = null) : base(comparer ?? Comparer<TKey>.Default)
		{
			this.keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
		}

		public T Peek()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			return this.First().Value.First;
		}

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			var (key, q) = this.First();
			if (q.Length == 1) Remove(key);
			return q.Pop();
		}

		public void Push(T item)
		{
			var key = keySelector(item);
			if (!TryGetValue(key, out var q)) this[key] = q = new Col.Queue<T>(99);
			q.Push(item);
		}
	}
}
