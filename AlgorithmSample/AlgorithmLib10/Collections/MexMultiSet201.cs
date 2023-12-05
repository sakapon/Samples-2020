namespace AlgorithmLib10.Collections.MexMultiSet201
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int max;
		readonly int[] counts;
		readonly int[] added;
		readonly PriorityQueue<int, int> pq;

		public MexMultiSet(int max)
		{
			this.max = max;
			counts = new int[max];
			added = new int[max];
			pq = new PriorityQueue<int, int>(Enumerable.Range(0, max).Select(v => (v, v)));
		}

		public int Mex => pq.Count == 0 ? max : pq.Peek();

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
			if (--counts[value] == 0) pq.Enqueue(value, value);
			return true;
		}

		void EnsureMex()
		{
			while (pq.Count > 0)
			{
				var value = pq.Peek();
				if (added[value] == 0) break;
				--added[value];
				pq.Dequeue();
			}
		}
	}
}
