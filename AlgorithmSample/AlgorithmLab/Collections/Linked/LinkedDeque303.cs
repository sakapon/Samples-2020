using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_D
// アンカーを一つだけ設置します。
namespace AlgorithmLab.Collections.Linked.LinkedDeque303
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

		readonly Node root = new Node();
		int n;

		public LinkedDeque(IEnumerable<T> items = null)
		{
			Clear();
			if (items != null) foreach (var item in items) AddLast(item);
		}

		public int Count => n;
		public T First
		{
			get => root.Next.Item;
			set => root.Next.Item = value;
		}
		public T Last
		{
			get => root.Previous.Item;
			set => root.Previous.Item = value;
		}

		public void Clear()
		{
			Node.Link(root, root);
			n = 0;
		}

		public void AddFirst(T item)
		{
			root.Next.AddPrevious(item);
			++n;
		}

		public void AddLast(T item)
		{
			root.AddPrevious(item);
			++n;
		}

		public T PopFirst()
		{
			var item = root.Next.Item;
			root.Next.Remove();
			--n;
			return item;
		}

		public T PopLast()
		{
			var item = root.Previous.Item;
			root.Previous.Remove();
			--n;
			return item;
		}

		public void ConcatFirst(LinkedDeque<T> other)
		{
			Node.Link(other.root.Previous, root.Next);
			Node.Link(root, other.root.Next);
			n += other.n;
			other.Clear();
		}

		public void ConcatLast(LinkedDeque<T> other)
		{
			Node.Link(root.Previous, other.root.Next);
			Node.Link(other.root.Previous, root);
			n += other.n;
			other.Clear();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var t = root.Next; t != root; t = t.Next) yield return t.Item; }

		public T[] ToArray()
		{
			var r = new T[n];
			for (var (t, i) = (root.Next, 0); t != root; t = t.Next, ++i) r[i] = t.Item;
			return r;
		}
	}
}
