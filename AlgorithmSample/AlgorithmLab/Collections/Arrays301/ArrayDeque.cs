using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
namespace AlgorithmLab.Collections.Arrays301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayDeque<T> : IEnumerable<T>
	{
		T[] a;
		int fi, li;
		int f;

		public ArrayDeque(int capacity = 2)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			a = new T[c];
			f = c - 1;
		}

		public ArrayDeque(IEnumerable<T> items) : this() { foreach (var item in items) AddLast(item); }
		public ArrayDeque(T[] items) => Initialize(items, items.Length);
		void Initialize(T[] a0, int c)
		{
			li = c;
			while (c != (c & -c)) c += c & -c;
			a = new T[c];
			Array.Copy(a0, a, li);
			f = c - 1;
		}

		public int Count => li - fi;
		public T this[int i]
		{
			get => a[fi + i & f];
			set => a[fi + i & f] = value;
		}
		public T First
		{
			get => a[fi & f];
			set => a[fi & f] = value;
		}
		public T Last
		{
			get => a[li - 1 & f];
			set => a[li - 1 & f] = value;
		}

		public void Clear() => fi = li = 0;
		public void AddFirst(T item)
		{
			if (li - fi == a.Length) Expand();
			a[--fi & f] = item;
		}
		public void AddLast(T item)
		{
			if (li - fi == a.Length) Expand();
			a[li++ & f] = item;
		}
		public T PopFirst() => a[fi++ & f];
		public T PopLast() => a[--li & f];

		void Expand()
		{
			Array.Resize(ref a, a.Length << 1);
			Array.Copy(a, 0, a, a.Length >> 1, a.Length >> 1);
			f = a.Length - 1;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = fi; i < li; ++i) yield return a[i & f]; }

		public T[] ToArray()
		{
			var r = new T[Count];
			for (int i = fi; i < li; ++i) r[i - fi] = a[i & f];
			return r;
		}
	}
}
