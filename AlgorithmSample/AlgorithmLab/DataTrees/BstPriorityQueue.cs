﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.DataTrees
{
	// 要素が重複しない (すべての値の順序が異なる) 場合に利用できます。
	public class DistinctPriorityQueue<T>
	{
		// 要素をそのままキーとして使用します。
		SortedSet<T> ss;

		public DistinctPriorityQueue() : this(Comparer<T>.Default) { }
		public DistinctPriorityQueue(IComparer<T> comparer) => ss = new SortedSet<T>(comparer);

		public int Count => ss.Count;

		public T Peek()
		{
			if (ss.Count == 0) throw new InvalidOperationException("The container is empty.");

			return ss.Min;
		}

		public T Pop()
		{
			if (ss.Count == 0) throw new InvalidOperationException("The container is empty.");

			var item = ss.Min;
			ss.Remove(item);
			return item;
		}

		public bool Push(T item) => ss.Add(item);
	}

	// 要素が重複する場合も利用できます。
	public class BstPriorityQueue<T>
	{
		// 要素をそのままキーとして使用します。
		SortedDictionary<T, int> sd;

		public BstPriorityQueue() : this(Comparer<T>.Default) { }
		public BstPriorityQueue(IComparer<T> comparer) => sd = new SortedDictionary<T, int>(comparer);

		public int Count { get; private set; }

		public T Peek()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			return sd.First().Key;
		}

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			Count--;
			var (item, count) = sd.First();
			if (count == 1) sd.Remove(item);
			else sd[item] = count - 1;
			return item;
		}

		public void Push(T item)
		{
			Count++;
			sd.TryGetValue(item, out var count);
			sd[item] = count + 1;
		}
	}

	// 要素に対してソート キーを指定する場合に利用します。
	public class KeyedPriorityQueue<T, TKey>
	{
		SortedDictionary<TKey, Queue<T>> sd;
		Func<T, TKey> keySelector;

		public KeyedPriorityQueue(Func<T, TKey> keySelector) : this(keySelector, Comparer<TKey>.Default) { }
		public KeyedPriorityQueue(Func<T, TKey> keySelector, IComparer<TKey> comparer)
		{
			this.keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
			sd = new SortedDictionary<TKey, Queue<T>>(comparer);
		}

		public int Count { get; private set; }

		public T Peek()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			return sd.First().Value.Peek();
		}

		public T Pop()
		{
			if (Count == 0) throw new InvalidOperationException("The container is empty.");

			Count--;
			var (key, q) = sd.First();
			if (q.Count == 1) sd.Remove(key);
			return q.Dequeue();
		}

		public void Push(T item)
		{
			Count++;
			var key = keySelector(item);
			Queue<T> q;
			if (!sd.TryGetValue(key, out q)) sd[key] = q = new Queue<T>();
			q.Enqueue(item);
		}
	}
}
