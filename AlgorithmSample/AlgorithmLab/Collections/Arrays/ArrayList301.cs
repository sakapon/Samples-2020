using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays.ArrayList301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayList<T> : IEnumerable<T>
	{
		const int DefaultCapacity = 2;
		T[] a;
		int n;

		public ArrayList(int capacity = DefaultCapacity)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			a = new T[c];
		}

		public ArrayList(ArrayList<T> list) => Initialize(list.a, list.n);
		public ArrayList(IEnumerable<T> items)
		{
			if (items is T[] a0)
			{
				Initialize(a0, a0.Length);
			}
			else
			{
				a = new T[DefaultCapacity];
				foreach (var item in items) Add(item);
			}
		}

		void Initialize(T[] a0, int n0)
		{
			n = n0;
			while (n0 != (n0 & -n0)) n0 += n0 & -n0;
			a = new T[n0];
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
	}
}
