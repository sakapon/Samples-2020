// 0-based, bit operations
// 非多重集合です。
// 遅延評価を使わずに直接削除します。
namespace AlgorithmLib10.DataTrees.PQ.ListHeapSet222
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHeapSet<T>
	{
		class Node
		{
			public T Key;
			public int Index;
		}

		readonly IComparer<T> c;
		readonly int s;
		readonly List<Node> l = new List<Node>();
		readonly Dictionary<T, Node> map = new Dictionary<T, Node>();

		public ListHeapSet(IEnumerable<T> items = null, IComparer<T> comparer = null, bool descending = false)
		{
			c = comparer ?? Comparer<T>.Default;
			s = descending ? -1 : 1;
			if (items != null) foreach (var x in items) Add(x);
		}

		public IComparer<T> Comparer => c;
		public bool Descending => s == -1;
		public int Count => l.Count;
		public T First => l.Count != 0 ? l[0].Key : throw new InvalidOperationException("No items.");

		public void Clear()
		{
			l.Clear();
			map.Clear();
		}

		public bool Contains(T item) => map.ContainsKey(item);

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (s * c.Compare(l[p].Key, l[i].Key) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			l[i].Index = i;
			l[p].Index = p;
			return true;
		}
		void UpHeap(int i) { for (; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap(int i) { for (i = (i << 1) | 1; i < l.Count && TrySwap(i + 1 < l.Count && s * c.Compare(l[i + 1].Key, l[i].Key) < 0 ? ++i : i); i = (i << 1) | 1) ; }

		public bool Add(T item)
		{
			if (map.ContainsKey(item)) return false;
			var node = new Node { Key = item, Index = l.Count };
			l.Add(node);
			map[item] = node;
			UpHeap(l.Count - 1);
			return true;
		}

		public bool RemoveFirst()
		{
			if (l.Count == 0) return false;
			return Remove(l[0].Key);
		}

		public bool Remove(T item)
		{
			if (!map.TryGetValue(item, out var node)) return false;
			var i = node.Index;
			l[i] = l[l.Count - 1];
			l[i].Index = i;
			l.RemoveAt(l.Count - 1);
			map.Remove(item);
			if (i != l.Count)
			{
				DownHeap(i);
				UpHeap(i);
			}
			return true;
		}
	}
}
