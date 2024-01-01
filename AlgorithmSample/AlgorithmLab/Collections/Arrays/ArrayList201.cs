using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_A
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_D
namespace AlgorithmLab.Collections.Arrays.ArrayList201
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

		public int Count => n;
		public T this[int i]
		{
			get => a[i];
			set => a[i] = value;
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
