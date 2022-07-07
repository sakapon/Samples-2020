using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// doubly linked list
// 両端にアンカーを設置します。
namespace AlgorithmLab.Collections.Linked.LinkedDeque202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LinkedDeque<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		class Node
		{
			public T Item;
			public Node Next, Previous;

			public void AddPrevious(T item)
			{
				var node = new Node { Item = item };
				Previous.Next = node;
				node.Previous = Previous;
				node.Next = this;
				Previous = node;
			}

			public void Remove()
			{
				Next.Previous = Previous;
				Previous.Next = Next;
			}
		}

		readonly Node fn = new Node(), ln = new Node();
		int n;

		public LinkedDeque() => Clear();

		public int Count => n;
		public T First
		{
			get => fn.Next.Item;
			set => fn.Next.Item = value;
		}
		public T Last
		{
			get => ln.Previous.Item;
			set => ln.Previous.Item = value;
		}

		public void Clear()
		{
			fn.Next = ln;
			ln.Previous = fn;
			n = 0;
		}

		public void AddFirst(T item)
		{
			fn.Next.AddPrevious(item);
			++n;
		}

		public void AddLast(T item)
		{
			ln.AddPrevious(item);
			++n;
		}

		public T PopFirst()
		{
			var item = fn.Next.Item;
			fn.Next.Remove();
			--n;
			return item;
		}

		public T PopLast()
		{
			var item = ln.Previous.Item;
			ln.Previous.Remove();
			--n;
			return item;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var t = fn.Next; t != ln; t = t.Next) yield return t.Item; }

		public T[] ToArray()
		{
			var r = new T[n];
			for (var (t, i) = (fn.Next, 0); t != ln; t = t.Next, ++i) r[i] = t.Item;
			return r;
		}
	}
}
