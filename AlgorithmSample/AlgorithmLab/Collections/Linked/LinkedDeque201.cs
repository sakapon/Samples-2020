using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// doubly linked list
namespace AlgorithmLab.Collections.Linked.LinkedDeque201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LinkedDeque<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		class Node
		{
			public T Item;
			public Node Next, Previous;
		}

		Node fn, ln;
		int n;

		public int Count => n;
		public T First
		{
			get => fn.Item;
			set => fn.Item = value;
		}
		public T Last
		{
			get => ln.Item;
			set => ln.Item = value;
		}

		public void Clear()
		{
			fn = ln = null;
			n = 0;
		}

		public void AddFirst(T item)
		{
			var node = new Node { Item = item, Next = fn };
			fn = (fn == null) ? (ln = node) : (fn.Previous = node);
			++n;
		}

		public void AddLast(T item)
		{
			var node = new Node { Item = item, Previous = ln };
			ln = (ln == null) ? (fn = node) : (ln.Next = node);
			++n;
		}

		public T PopFirst()
		{
			var item = fn.Item;
			if ((fn = fn.Next) == null) ln = null;
			--n;
			return item;
		}

		public T PopLast()
		{
			var item = ln.Item;
			if ((ln = ln.Previous) == null) fn = null;
			--n;
			return item;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var t = fn; t != null; t = t.Next) yield return t.Item; }

		public T[] ToArray()
		{
			var r = new T[n];
			for (var (t, i) = (fn, 0); t != null; t = t.Next, ++i) r[i] = t.Item;
			return r;
		}
	}
}
