namespace AlgorithmLib10.Collections.MexSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexSet
	{
		readonly int _max;
		readonly SortedSet<int> _ex;

		public MexSet(int max)
		{
			_max = max;
			_ex = new SortedSet<int>(Enumerable.Range(0, max));
		}

		public int Mex => _ex.Count == 0 ? _max : _ex.Min;

		public bool Add(int value)
		{
			if (value < 0 || _max <= value) return false;
			return _ex.Remove(value);
		}

		public bool Remove(int value)
		{
			if (value < 0 || _max <= value) return false;
			return _ex.Add(value);
		}
	}
}
