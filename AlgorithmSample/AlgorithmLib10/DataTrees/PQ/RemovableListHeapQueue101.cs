// 0-based
namespace AlgorithmLib10.DataTrees.PQ.RemovableListHeapQueue101
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class RemovableListHeapQueue<T>
	{
		readonly IComparer<T> c;
		readonly List<T> l = new List<T>();
		int n;
		readonly Dictionary<T, int> counts = new Dictionary<T, int>();

		public RemovableListHeapQueue(IEnumerable<T> items = null, IComparer<T> comparer = null)
		{
			c = comparer ?? Comparer<T>.Default;
			if (items != null) foreach (var x in items) Push(x);
		}

		public IComparer<T> Comparer => c;
		public int Count => n;
		public T First => n != 0 ? l[0] : throw new InvalidOperationException("No items.");

		public void Clear()
		{
			l.Clear();
			n = 0;
			counts.Clear();
		}

		public int GetCount(T item) => counts.GetValueOrDefault(item);

		bool TrySwap(int i)
		{
			var p = (i - 1) / 2;
			if (c.Compare(l[p], l[i]) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			return true;
		}

		void UpHeap()
		{
			for (var i = l.Count - 1; i > 0; i = (i - 1) / 2)
			{
				if (!TrySwap(i)) break;
			}
		}

		void DownHeap()
		{
			for (var i = 1; i < l.Count; i = i * 2 + 1)
			{
				if (i + 1 < l.Count && c.Compare(l[i + 1], l[i]) < 0) ++i;
				if (!TrySwap(i)) break;
			}
		}

		public void Push(T item)
		{
			l.Add(item);
			UpHeap();
			++n;
			counts[item] = counts.GetValueOrDefault(item) + 1;
		}

		public T Pop()
		{
			if (n == 0) throw new InvalidOperationException("No items.");
			var item = l[0];
			Remove(item);
			return item;
		}

		public bool Remove(T item)
		{
			if (!counts.TryGetValue(item, out var c)) return false;
			if (--c == 0) counts.Remove(item); else counts[item] = c;
			--n;
			EnsureFirst();
			return true;
		}

		void EnsureFirst()
		{
			while (l.Count > 0 && !counts.ContainsKey(l[0]))
			{
				l[0] = l[l.Count - 1];
				l.RemoveAt(l.Count - 1);
				DownHeap();
			}
		}
	}
}
