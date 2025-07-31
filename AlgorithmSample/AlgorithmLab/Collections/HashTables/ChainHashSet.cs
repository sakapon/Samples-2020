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
}
