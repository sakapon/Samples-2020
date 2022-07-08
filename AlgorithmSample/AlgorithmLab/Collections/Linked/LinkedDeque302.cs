using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_D
namespace AlgorithmLab.Collections.Linked.LinkedDeque302
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LinkedDeque<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		class Node
		{
			public T Item;
			public Node Next, Previous;

			public static void Link(Node previous, Node next)
			{
				previous.Next = next;
				next.Previous = previous;
			}

			public Node AddPrevious(T item)
			{
				var node = new Node { Item = item };
				Link(Previous, node);
				Link(node, this);
				return node;
			}

			public Node Remove()
			{
				Link(Previous, Next);
				return Next;
			}
		}

		readonly Node fn = new Node(), ln = new Node();
		int n;

		public LinkedDeque(IEnumerable<T> items = null)
		{
			Clear();
			if (items != null) foreach (var item in items) AddLast(item);
		}

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
			Node.Link(fn, ln);
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

		public void ConcatFirst(LinkedDeque<T> other)
		{
			Node.Link(other.ln.Previous, fn.Next);
			Node.Link(fn, other.fn.Next);
			n += other.n;
			other.Clear();
		}

		public void ConcatLast(LinkedDeque<T> other)
		{
			Node.Link(ln.Previous, other.fn.Next);
			Node.Link(other.ln.Previous, ln);
			n += other.n;
			other.Clear();
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
