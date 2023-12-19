namespace AlgorithmLib10.Collections.MexSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexSet
	{
		readonly int max;
		readonly SortedSet<int> ex;

		public MexSet(int max)
		{
			this.max = max;
			ex = new SortedSet<int>(Enumerable.Range(0, max));
		}

		public int Mex => ex.Count == 0 ? max : ex.Min;

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			return ex.Remove(value);
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			return ex.Add(value);
		}
	}
}
