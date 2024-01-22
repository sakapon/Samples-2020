using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashSet<T> : IEnumerable<T>
	{
		int n, f;
		int[] lasts;
		bool[] u;
		int[] hashes;
		T[] a;
		readonly IEqualityComparer<T> ec = typeof(T) == typeof(string) ? (IEqualityComparer<T>)StringComparer.Ordinal : EqualityComparer<T>.Default;

		public ArrayHashSet(int capacity = 8)
		{
			var c = 1;
			while (c < capacity) c <<= 1;

			f = c - 1;
			lasts = new int[c];
			for (int i = 0; i < c; ++i) lasts[i] = i;
			u = new bool[c];
			hashes = new int[c];
			a = new T[c];
		}

		public int Count => n;

		public void Clear()
		{
			n = 0;
			for (int i = 0; i < lasts.Length; ++i) lasts[i] = i;
			Array.Clear(u, 0, u.Length);
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			for (int i = h; i != lasts[h]; ++i)
				if (u[i & f] && hashes[i & f] == h && ec.Equals(a[i & f], item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			if (n == a.Length >> 1) Expand();

			var h = item.GetHashCode() & f;
			for (int i = h; ; ++i)
			{
				if (!u[i & f])
				{
					++n;
					if (lasts[h] <= i) lasts[h] = i + 1;
					u[i & f] = true;
					hashes[i & f] = h;
					a[i & f] = item;
					return true;
				}
				else
				{
					if (hashes[i & f] == h && ec.Equals(a[i & f], item)) return false;
				}
			}
			throw new InvalidOperationException();
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			for (int i = h; i != lasts[h]; ++i)
			{
				if (u[i & f] && hashes[i & f] == h && ec.Equals(a[i & f], item))
				{
					--n;
					u[i & f] = false;
					return true;
				}
			}
			return false;
		}

		void Expand()
		{
			var (tu, ta) = (u, a);
			var c = lasts.Length << 1;

			n = 0;
			f = c - 1;
			lasts = new int[c];
			for (int i = 0; i < c; ++i) lasts[i] = i;
			u = new bool[c];
			hashes = new int[c];
			a = new T[c];

			for (var i = 0; i < tu.Length; ++i) if (tu[i]) Add(ta[i]);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i <= f; ++i) if (u[i]) yield return a[i]; }
	}
}
