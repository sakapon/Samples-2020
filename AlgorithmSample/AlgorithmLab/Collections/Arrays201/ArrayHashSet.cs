using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashSet<T> : IEnumerable<T>
	{
		int n, f;
		int[] lasts;
		int?[] hashes;
		T[] a;
		readonly EqualityComparer<T> ec = EqualityComparer<T>.Default;

		public ArrayHashSet(int capacity = 8)
		{
			var c = 1;
			while (c < capacity) c <<= 1;

			f = c - 1;
			lasts = new int[c];
			for (int i = 0; i < c; ++i) lasts[i] = i;
			hashes = new int?[c];
			a = new T[c];
		}

		public int Count => n;

		public void Clear()
		{
			n = 0;
			for (int i = 0; i < lasts.Length; ++i) lasts[i] = i;
			Array.Clear(hashes, 0, hashes.Length);
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			for (int i = h; i != lasts[h]; ++i)
				if (hashes[i & f] == h && ec.Equals(a[i & f], item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			if (n == a.Length >> 1) Expand();

			var h = item.GetHashCode() & f;
			for (int i = h; ; ++i)
			{
				if (hashes[i & f] is null)
				{
					++n;
					if (lasts[h] <= i) lasts[h] = i + 1;
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

		public bool Remove(T key)
		{
			throw new NotImplementedException();
		}

		void Expand()
		{
			var (th, ta) = (hashes, a);
			var c = lasts.Length << 1;

			n = 0;
			f = c - 1;
			lasts = new int[c];
			for (int i = 0; i < c; ++i) lasts[i] = i;
			hashes = new int?[c];
			a = new T[c];

			for (var i = 0; i < th.Length; ++i) if (!(th[i] is null)) Add(ta[i]);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i <= f; ++i) if (!(hashes[i] is null)) yield return a[i]; }
	}
}
