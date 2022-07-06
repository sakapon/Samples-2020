// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_A
// singly linked list
namespace AlgorithmLab.Collections.Linked.LinkedStack101
{
	public class LinkedStack<T>
	{
		class Node
		{
			public T Item;
			public Node Next;
		}

		Node fn;
		int n;

		public int Count => n;
		public T First
		{
			get => fn.Item;
			set => fn.Item = value;
		}

		public void Clear()
		{
			fn = null;
			n = 0;
		}

		public void Add(T item)
		{
			fn = new Node { Item = item, Next = fn };
			++n;
		}

		public T Pop()
		{
			var item = fn.Item;
			fn = fn.Next;
			--n;
			return item;
		}
	}
}
