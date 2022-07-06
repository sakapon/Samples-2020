using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays.ArrayList301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayList<T> : IEnumerable<T>
	{
		T[] a;
		int n;

		public ArrayList(int capacity = 2)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			a = new T[c];
		}

		public ArrayList(IEnumerable<T> items) : this() { foreach (var item in items) Add(item); }
		public ArrayList(T[] items) => Initialize(items, items.Length);
		public ArrayList(ArrayList<T> list) => Initialize(list.a, list.n);
		void Initialize(T[] a0, int c)
		{
			n = c;
			while (c != (c & -c)) c += c & -c;
			a = new T[c];
			Array.Copy(a0, a, n);
		}

		public int Count => n;
		public T this[int i]
		{
			get => a[i];
			set => a[i] = value;
		}
		public T First
		{
			get => a[0];
			set => a[0] = value;
		}
		public T Last
		{
			get => a[n - 1];
			set => a[n - 1] = value;
		}

		public void Clear() => n = 0;
		public void Add(T item)
		{
			if (n == a.Length) Expand();
			a[n++] = item;
		}
		public T Pop() => a[--n];

		public void Insert(int i, T item)
		{
			if (n == a.Length) Expand();
			Array.Copy(a, i, a, i + 1, n++ - i);
			a[i] = item;
		}

		public T RemoveAt(int i)
		{
			var item = a[i];
			Array.Copy(a, i + 1, a, i, --n - i);
			return item;
		}

		void Expand()
		{
			Array.Resize(ref a, a.Length << 1);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i < n; ++i) yield return a[i]; }

		public T[] ToArray()
		{
			var r = new T[n];
			Array.Copy(a, r, n);
			return r;
		}

		public void Reverse() => Array.Reverse(a, 0, n);
	}
}
