namespace AlgorithmLib10.Collections.MexMultiSet206
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int max;
		readonly int[] counts;
		readonly IntPQSet ex;

		public MexMultiSet(int max)
		{
			this.max = max;
			counts = new int[max];
			ex = new IntPQSet(max, Enumerable.Range(0, max));
		}

		public int Mex => ex.Count == 0 ? max : ex.Min;

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value]++ == 0) ex.Remove(value);
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) ex.Add(value);
			return true;
		}
	}

	class IntPQSet : PriorityQueue<int, int>
	{
		readonly int[] counts;

		public IntPQSet(int max, IEnumerable<int> items)
		{
			counts = new int[max];
			if (items != null) foreach (var v in items) Add(v);
		}

		public int Min => Peek();

		public bool Add(int item)
		{
			Enqueue(item, item);
			++counts[item];
			return true;
		}

		public bool Remove(int item)
		{
			if (counts[item] == 0) return false;
			--counts[item];
			EnsureMin();
			return true;
		}

		void EnsureMin()
		{
			while (Count > 0 && counts[Peek()] == 0) Dequeue();
		}
	}
}
