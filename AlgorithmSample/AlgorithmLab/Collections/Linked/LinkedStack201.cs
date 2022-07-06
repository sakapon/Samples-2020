using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Linked.LinkedStack201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LinkedStack<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		class Node
		{
			public T Item;
			public Node Next;
		}

		Node First;
		public int Count { get; private set; }

		public T Top => First != null ? First.Item : throw new InvalidOperationException("There are no items.");

		public T Pop()
		{
			var item = Top;
			First = First.Next;
			--Count;
			return item;
		}

		public void Add(T item)
		{
			First = new Node { Item = item, Next = First };
			++Count;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var n = First; n != null; n = n.Next) yield return n.Item; }
	}
}
