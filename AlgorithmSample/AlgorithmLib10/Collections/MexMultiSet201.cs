namespace AlgorithmLib10.Collections.MexMultiSet201
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int _max;
		readonly int[] _counts;
		readonly int[] _added;
		readonly PriorityQueue<int, int> _ex;

		public MexMultiSet(int max)
		{
			_max = max;
			_counts = new int[max];
			_added = new int[max];
			_ex = new PriorityQueue<int, int>(Enumerable.Range(0, max).Select(v => (v, v)));
		}

		public int Mex => _ex.Count == 0 ? _max : _ex.Peek();

		public bool Add(int value)
		{
			if (value < 0 || _max <= value) return false;
			if (_counts[value]++ == 0)
			{
				++_added[value];
				EnsureMex();
			}
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || _max <= value) return false;
			if (_counts[value] == 0) return false;
			if (--_counts[value] == 0) _ex.Enqueue(value, value);
			return true;
		}

		void EnsureMex()
		{
			while (_ex.Count > 0)
			{
				var value = _ex.Peek();
				if (_added[value] == 0) break;
				--_added[value];
				_ex.Dequeue();
			}
		}
	}
}
