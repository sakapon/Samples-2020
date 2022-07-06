using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_A
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_A
namespace AlgorithmLab.Collections.Arrays.ArrayStack201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayStack<T> : IEnumerable<T>
	{
		T[] a;
		int n;

		public ArrayStack(int capacity = 2)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			a = new T[c];
		}

		public int Count => n;
		public T this[int i]
		{
			get => a[n - 1 - i];
			set => a[n - 1 - i] = value;
		}
		public T First
		{
			get => a[n - 1];
			set => a[n - 1] = value;
		}

		public void Clear() => n = 0;
		public void Push(T item)
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
		public IEnumerator<T> GetEnumerator() { for (var i = n - 1; i >= 0; --i) yield return a[i]; }

		public T[] ToArray()
		{
			var r = new T[n];
			Array.Copy(a, r, n);
			Array.Reverse(r);
			return r;
		}
	}
}
