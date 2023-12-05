namespace AlgorithmLib10.Collections.MexSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexSet
	{
		readonly int max;
		readonly SortedSet<int> set;

		public MexSet(int max)
		{
			this.max = max;
			set = new SortedSet<int>(Enumerable.Range(0, max));
		}

		public int Mex => set.Count == 0 ? max : set.Min;

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			return set.Remove(value);
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			return set.Add(value);
		}
	}
}
