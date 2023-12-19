namespace AlgorithmLib10.Collections.MexMultiSet201
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int max;
		readonly int[] counts;
		readonly int[] added;
		readonly PriorityQueue<int, int> ex;

		public MexMultiSet(int max)
		{
			this.max = max;
			counts = new int[max];
			added = new int[max];
			ex = new PriorityQueue<int, int>(Enumerable.Range(0, max).Select(v => (v, v)));
		}

		public int Mex => ex.Count == 0 ? max : ex.Peek();

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value]++ == 0)
			{
				++added[value];
				EnsureMex();
			}
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) ex.Enqueue(value, value);
			return true;
		}

		void EnsureMex()
		{
			while (ex.Count > 0)
			{
				var value = ex.Peek();
				if (added[value] == 0) break;
				--added[value];
				ex.Dequeue();
			}
		}
	}
}
