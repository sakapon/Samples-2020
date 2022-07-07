// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// doubly linked list
namespace AlgorithmLab.Collections.Linked.LinkedDeque101
{
	public class LinkedDeque<T>
	{
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
			else fn.Previous = null;
			--n;
			return item;
		}

		public T PopLast()
		{
			var item = ln.Item;
			if ((ln = ln.Previous) == null) fn = null;
			else ln.Next = null;
			--n;
			return item;
		}
	}
}
