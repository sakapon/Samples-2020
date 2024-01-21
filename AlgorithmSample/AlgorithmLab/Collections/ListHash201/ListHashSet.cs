using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash201
{
	public class ListHashSet<T>
	{
		readonly List<T>[] t;
		readonly int f;
		int n;

		public ListHashSet(int capacity)
		{
			t = new List<T>[capacity];
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			Array.Clear(t, 0, t.Length);
			n = 0;
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			return l != null && l.Contains(item);
		}

		public bool Add(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			if (l != null && l.Contains(item)) return false;
			if (l == null) t[h] = new List<T> { item };
			else l.Add(item);
			++n;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			var b = l != null && l.Remove(item);
			if (b) --n;
			return b;
		}
	}
}
