using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashSet<T> : IEnumerable<T>
	{
		int n, f;
		int[] firsts, nexts;
		bool[] u;
		T[] a;
		readonly IEqualityComparer<T> ec = typeof(T) == typeof(string) ? (IEqualityComparer<T>)StringComparer.Ordinal : EqualityComparer<T>.Default;

		public ArrayHashSet(int capacity = 8)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			Initialize(c);
		}

		void Initialize(int c)
		{
			n = 0;
			f = c - 1;
			firsts = new int[c];
			nexts = new int[c];
			u = new bool[c];
			a = new T[c];
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
		}

		public int Count => n;

		public void Clear() => Initialize(8);

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(a[i], item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			if (n == a.Length >> 1) Expand();
			return Add0(item);
		}

		bool Add0(T item)
		{
			var h = item.GetHashCode() & f;
			var last = -1;
			var i = firsts[h];
			for (; i != -1; last = i, i = nexts[i])
				if (ec.Equals(a[i], item)) return false;

			i = last == -1 ? h : (last + 1) & f;
			while (u[i]) i = (i + 1) & f;

			++n;
			if (last == -1) firsts[h] = i;
			else nexts[last] = i;
			u[i] = true;
			a[i] = item;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var last = -1;
			for (var i = firsts[h]; i != -1; last = i, i = nexts[i])
			{
				if (!ec.Equals(a[i], item)) continue;

				--n;
				if (last == -1) firsts[h] = nexts[i];
				else nexts[last] = nexts[i];
				nexts[i] = -1;
				u[i] = false;
				return true;
			}
			return false;
		}

		void Expand()
		{
			var (tu, ta) = (u, a);
			Initialize(a.Length << 1);
			for (var i = 0; i < tu.Length; ++i) if (tu[i]) Add0(ta[i]);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i <= f; ++i) if (u[i]) yield return a[i]; }

		public T[] ToArray()
		{
			var r = new T[n];
			for (int i = 0, j = 0; i <= f; ++i) if (u[i]) r[j++] = a[i];
			return r;
		}
	}
}
