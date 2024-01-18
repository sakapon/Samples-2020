using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays101
{
	public class ArrayHashSet<T>
	{
		readonly List<T>[] map;
		readonly int f;
		int n;

		public ArrayHashSet(int capacity)
		{
			map = new List<T>[capacity];
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			Array.Clear(map, 0, map.Length);
			n = 0;
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			var l = map[h];
			return l != null && l.Contains(item);
		}

		public bool Add(T item)
		{
			var h = item.GetHashCode() & f;
			var l = map[h];
			if (l != null && l.Contains(item)) return false;
			if (l == null) map[h] = new List<T> { item };
			else l.Add(item);
			++n;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var l = map[h];
			var b = l != null && l.Remove(item);
			if (b) --n;
			return b;
		}
	}
}
