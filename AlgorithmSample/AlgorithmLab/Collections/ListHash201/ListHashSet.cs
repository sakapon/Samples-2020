using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHashSet<T> : IEnumerable<T>
	{
		List<T>[] t;
		int n, f;
		readonly IEqualityComparer<T> ec = typeof(T) == typeof(string) ? (IEqualityComparer<T>)StringComparer.Ordinal : EqualityComparer<T>.Default;

		public ListHashSet(int capacity = 8)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			Initialize(c);
		}

		public int Count => n;
		public void Clear() => Initialize(8);
		void Initialize(int c)
		{
			t = new List<T>[c];
			n = 0;
			f = c - 1;
		}

		int GetIndex(List<T> l, T item)
		{
			if (l == null) return -1;
			for (int i = 0; i < l.Count; ++i)
				if (ec.Equals(l[i], item)) return i;
			return -1;
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			return GetIndex(t[h], item) != -1;
		}

		public bool Add(T item)
		{
			if (n == t.Length >> 1) Expand();
			return Add0(item);
		}

		bool Add0(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			if (GetIndex(l, item) != -1) return false;
			if (l == null) t[h] = new List<T> { item };
			else l.Add(item);
			++n;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			var i = GetIndex(l, item);
			if (i == -1) return false;
			l.RemoveAt(i);
			--n;
			return true;
		}

		void Expand()
		{
			var tt = t;
			Initialize(t.Length << 1);
			foreach (var l in tt)
				if (l != null) for (int i = 0; i < l.Count; ++i) Add0(l[i]);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			foreach (var l in t)
				if (l != null) for (int i = 0; i < l.Count; ++i) yield return l[i];
		}

		public T[] ToArray()
		{
			var r = new T[n];
			var j = 0;
			foreach (var l in t)
				if (l != null) for (int i = 0; i < l.Count; ++i) r[j++] = l[i];
			return r;
		}
	}
}
