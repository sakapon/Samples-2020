// 0-based, v in [0, max)
namespace AlgorithmLib10.DataTrees.PQ.IntRemovableListHeapQueue101
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IntRemovableListHeapQueue
	{
		readonly List<int> l = new List<int>();
		int count;
		readonly int[] counts;

		public int Count => count;
		public int First => count != 0 ? l[0] : throw new InvalidOperationException("No items.");
		public List<int> Raw => l;

		public IntRemovableListHeapQueue(int max, IEnumerable<int> items)
		{
			counts = new int[max];
			if (items != null) foreach (var v in items) Push(v);
		}

		public void Clear()
		{
			l.Clear();
			count = 0;
			Array.Clear(counts, 0, counts.Length);
		}

		public int GetCount(int item) => counts[item];

		bool TrySwap(int i)
		{
			var p = (i - 1) / 2;
			if (l[p] <= l[i]) return false;
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
				if (i + 1 < l.Count && l[i + 1] < l[i]) ++i;
				if (!TrySwap(i)) break;
			}
		}

		public void Push(int item)
		{
			l.Add(item);
			UpHeap();
			++count;
			++counts[item];
		}

		public int Pop()
		{
			if (l.Count == 0) throw new InvalidOperationException("No items.");
			var item = l[0];
			Remove(item);
			return item;
		}

		public bool Remove(int item)
		{
			if (counts[item] == 0) return false;
			--count;
			--counts[item];
			EnsureFirst();
			return true;
		}

		void EnsureFirst()
		{
			while (l.Count > 0 && counts[l[0]] == 0)
			{
				while (l.Count > 0 && counts[l[l.Count - 1]] == 0) l.RemoveAt(l.Count - 1);
				if (l.Count == 0) break;
				l[0] = l[l.Count - 1];
				l.RemoveAt(l.Count - 1);
				DownHeap();
			}
		}
	}
}
