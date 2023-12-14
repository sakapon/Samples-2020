namespace AlgorithmLib10.Collections.MexMultiSet206
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int _max;
		readonly int[] _counts;
		readonly IntPQSet _ex;

		public MexMultiSet(int max)
		{
			_max = max;
			_counts = new int[max];
			_ex = new IntPQSet(max, Enumerable.Range(0, max));
		}

		public int Mex => _ex.Count == 0 ? _max : _ex.Min;

		public bool Add(int value)
		{
			if (value < 0 || _max <= value) return false;
			if (_counts[value]++ == 0) _ex.Remove(value);
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || _max <= value) return false;
			if (_counts[value] == 0) return false;
			if (--_counts[value] == 0) _ex.Add(value);
			return true;
		}
	}

	class IntPQSet : PriorityQueue<int, int>
	{
		readonly int[] _counts;

		public IntPQSet(int max, IEnumerable<int> items) : base(items.Select(v => (v, v)))
		{
			_counts = new int[max];
			if (items != null)
				foreach (var v in items) ++_counts[v];
		}

		public int Min => Peek();

		public bool Add(int item)
		{
			Enqueue(item, item);
			++_counts[item];
			return true;
		}

		public bool Remove(int item)
		{
			if (_counts[item] == 0) return false;
			--_counts[item];
			EnsureMin();
			return true;
		}

		void EnsureMin()
		{
			while (Count > 0 && _counts[Peek()] == 0)
			{
				Dequeue();
			}
		}
	}
}
