using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash101
{
	public class ListHashSet<T>
	{
		List<T>[] t;
		readonly int f;
		int n;

		public ListHashSet(int capacity)
		{
			t = Array.ConvertAll(new bool[capacity], _ => new List<T>());
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			t = Array.ConvertAll(t, _ => new List<T>());
			n = 0;
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			return l.Contains(item);
		}

		public bool Add(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			if (l.Contains(item)) return false;
			l.Add(item);
			++n;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			var b = l.Remove(item);
			if (b) --n;
			return b;
		}
	}
}
