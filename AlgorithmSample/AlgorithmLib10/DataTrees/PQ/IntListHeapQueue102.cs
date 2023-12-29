// 0-based, bit operations
namespace AlgorithmLib10.DataTrees.PQ.IntListHeapQueue102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IntListHeapQueue
	{
		readonly List<int> l = new List<int>();
		public int Count => l.Count;
		public int First => l.Count != 0 ? l[0] : throw new InvalidOperationException("No items.");
		public List<int> Raw => l;
		public void Clear() => l.Clear();

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (l[p] <= l[i]) return false;
			(l[i], l[p]) = (l[p], l[i]);
			return true;
		}
		void UpHeap() { for (var i = l.Count - 1; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap() { for (var i = 1; i < l.Count && TrySwap(i + 1 < l.Count && l[i + 1] < l[i] ? ++i : i); i = (i << 1) | 1) ; }

		public void Push(int item)
		{
			l.Add(item);
			UpHeap();
		}

		public int Pop()
		{
			if (l.Count == 0) throw new InvalidOperationException("No items.");
			var item = l[0];
			l[0] = l[l.Count - 1];
			l.RemoveAt(l.Count - 1);
			DownHeap();
			return item;
		}
	}
}
