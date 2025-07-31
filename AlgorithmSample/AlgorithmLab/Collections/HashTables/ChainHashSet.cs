using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.HashTables
{
	public class ChainHashSet<T>
	{
		static readonly double a = (Math.Sqrt(5) - 1) / 2;

		class Node
		{
			public T Item;
			public Node Next;
		}

		Node[] nodes;
		int size;
		public IEqualityComparer<T> Comparer { get; }
		public int Count { get; private set; }

		public ChainHashSet(int size, IEqualityComparer<T> comparer = null)
		{
			nodes = new Node[this.size = size];
			Comparer = comparer ?? EqualityComparer<T>.Default;
		}

		int Hash(T item)
		{
			double k = item?.GetHashCode() ?? 0;
			k *= a;
			k -= Math.Floor(k);
			k *= size;
			return (int)k;
		}

		public bool Contains(T item)
		{
			var h = Hash(item);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Item, item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			var h = Hash(item);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Item, item)) return false;

			nodes[h] = new Node { Item = item, Next = nodes[h] };
			++Count;
			return true;
		}

		public bool Remove(T item)
		{
			var h = Hash(item);
			for (ref var n = ref nodes[h]; n != null; n = ref n.Next)
				if (Remove(ref n, item)) return true;
			return false;
		}

		bool Remove(ref Node n, T item)
		{
			if (!Comparer.Equals(n.Item, item)) return false;
			n = n.Next;
			--Count;
			return true;
		}
	}

	public class ChainHashMap<TKey, TValue>
	{
		static readonly double A = (Math.Sqrt(5) - 1) / 2;

		class Node
		{
			public TKey Key;
			public TValue Value;
			public Node Next;
		}

		Node[] nodes;
		int size;
		public IEqualityComparer<TKey> Comparer { get; }
		public int Count { get; private set; }

		public ChainHashMap(int size, IEqualityComparer<TKey> comparer = null)
		{
			nodes = new Node[this.size = size];
			Comparer = comparer ?? EqualityComparer<TKey>.Default;
		}

		public TValue this[TKey key]
		{
			get => GetNode(key).Value;
			set
			{
				var n = GetNode(key);
				if (n == null) Add(key, value);
				else n.Value = value;
			}
		}

		int Hash(TKey key)
		{
			double k = key?.GetHashCode() ?? 0;
			k *= A;
			k -= Math.Floor(k);
			k *= size;
			return (int)k;
		}

		Node GetNode(TKey key)
		{
			var h = Hash(key);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Key, key)) return n;
			return null;
		}

		public bool ContainsKey(TKey key)
		{
			return GetNode(key) != null;
		}

		public bool Add(TKey key, TValue value)
		{
			var h = Hash(key);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Key, key)) return false;

			nodes[h] = new Node { Key = key, Value = value, Next = nodes[h] };
			++Count;
			return true;
		}

		public bool Remove(TKey key)
		{
			var h = Hash(key);
			for (ref var n = ref nodes[h]; n != null; n = ref n.Next)
				if (Remove(ref n, key)) return true;
			return false;
		}

		bool Remove(ref Node n, TKey key)
		{
			if (!Comparer.Equals(n.Key, key)) return false;
			n = n.Next;
			--Count;
			return true;
		}
	}
}
