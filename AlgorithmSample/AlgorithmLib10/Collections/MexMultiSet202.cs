namespace AlgorithmLib10.Collections.MexMultiSet202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly bool errorForArgs;
		readonly int max;
		readonly int[] counts;
		readonly int[] added;
		readonly (int, int)[] range;
		PriorityQueue<int, int> pq;

		public MexMultiSet(int max, IEnumerable<int> collection = null, bool errorForArgs = false)
		{
			this.errorForArgs = errorForArgs;
			this.max = max;
			counts = new int[max];
			added = new int[max];
			range = new (int, int)[max];
			for (int i = 0; i < max; ++i) range[i] = (i, i);
			Clear();
			if (collection != null) foreach (var v in collection) Add(v);
		}

		public int Mex => pq.Count == 0 ? max : pq.Peek();
		public int Count { get; private set; }

		public void Clear()
		{
			pq = new PriorityQueue<int, int>(range);
		}

		bool Validate(int value)
		{
			if (0 <= value && value < max) return true;
			if (errorForArgs) throw new ArgumentOutOfRangeException(nameof(value));
			else return false;
		}

		public bool Add(int value)
		{
			if (!Validate(value)) return false;
			if (counts[value]++ == 0)
			{
				++added[value];
				EnsureMex();
			}
			++Count;
			return true;
		}

		public bool Remove(int value)
		{
			if (!Validate(value)) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) pq.Enqueue(value, value);
			--Count;
			return true;
		}

		public int GetCount(int value)
		{
			if (!Validate(value)) return -1;
			return counts[value];
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
