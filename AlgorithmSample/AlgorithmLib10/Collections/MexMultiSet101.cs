namespace AlgorithmLib10.Collections.MexMultiSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int _max;
		readonly int[] _counts;
		readonly SortedSet<int> _ex;

		public MexMultiSet(int max)
		{
			_max = max;
			_counts = new int[max];
			_ex = new SortedSet<int>(Enumerable.Range(0, max));
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
}
