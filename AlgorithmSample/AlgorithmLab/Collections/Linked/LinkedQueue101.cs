// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// singly linked list
namespace AlgorithmLab.Collections.Linked.LinkedQueue101
{
	public class LinkedQueue<T>
	{
		class Node
		{
			public T Item;
			public Node Next;
		}

		Node fn, ln;
		int n;

		public int Count => n;
		public T First
		{
			get => fn.Item;
			set => fn.Item = value;
		}

		public void Clear()
		{
			fn = ln = null;
			n = 0;
		}

		public void Add(T item)
		{
			var node = new Node { Item = item };
			ln = (ln == null) ? (fn = node) : (ln.Next = node);
			++n;
		}

		public T Pop()
		{
			var item = fn.Item;
			if ((fn = fn.Next) == null) ln = null;
			--n;
			return item;
		}
	}
}
