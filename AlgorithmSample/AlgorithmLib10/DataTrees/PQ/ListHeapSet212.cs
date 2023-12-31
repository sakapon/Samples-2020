// 0-based, bit operations
// 非多重集合です。
// 遅延評価を使わずに直接削除します。
namespace AlgorithmLib10.DataTrees.PQ.ListHeapSet212
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHeapSet<T>
	{
		readonly IComparer<T> c;
		readonly int s;
		readonly List<T> l = new List<T>();
		readonly Dictionary<T, int> map = new Dictionary<T, int>();

		public ListHeapSet(IEnumerable<T> items = null, IComparer<T> comparer = null, bool descending = false)
		{
			c = comparer ?? Comparer<T>.Default;
			s = descending ? -1 : 1;
			if (items != null) foreach (var x in items) Add(x);
		}

		public IComparer<T> Comparer => c;
		public bool Descending => s == -1;
		public List<T> Raw => l;
		public int Count => l.Count;
		public T First => l.Count != 0 ? l[0] : throw new InvalidOperationException("No items.");

		public void Clear()
		{
			l.Clear();
			map.Clear();
		}

		public bool Contains(T item) => map.ContainsKey(item);

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (s * c.Compare(l[p], l[i]) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			map[l[i]] = i;
			map[l[p]] = p;
			return true;
		}
		void UpHeap(int i) { for (; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap(int i) { for (i = (i << 1) | 1; i < l.Count && TrySwap(i + 1 < l.Count && s * c.Compare(l[i + 1], l[i]) < 0 ? ++i : i); i = (i << 1) | 1) ; }

		public bool Add(T item)
		{
			if (map.ContainsKey(item)) return false;
			map[item] = l.Count;
			l.Add(item);
			UpHeap(l.Count - 1);
			return true;
		}

		public bool RemoveFirst()
		{
			if (l.Count == 0) return false;
			return Remove(l[0]);
		}

		public bool Remove(T item)
		{
			var i = map.GetValueOrDefault(item, -1);
			if (i == -1) return false;
			l[i] = l[l.Count - 1];
			map[l[i]] = i;
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
