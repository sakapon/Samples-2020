namespace AlgorithmLib10.Collections.MexSet102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, Mex = {Mex}")]
	public class MexSet
	{
		readonly bool errorForArgs;
		readonly int max;
		readonly int[] range;
		SortedSet<int> ex;

		public MexSet(int max, IEnumerable<int> collection = null, bool errorForArgs = false)
		{
			this.errorForArgs = errorForArgs;
			this.max = max;
			range = new int[max];
			for (int i = 0; i < max; ++i) range[i] = i;
			Clear();
			if (collection != null) foreach (var v in collection) Add(v);
		}

		public int Mex => ex.Count == 0 ? max : ex.Min;
		public int Count => max - ex.Count;

		public void Clear()
		{
			ex = new SortedSet<int>(range);
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
			return ex.Remove(value);
		}

		public bool Remove(int value)
		{
			if (!Validate(value)) return false;
			return ex.Add(value);
		}

		public bool Contains(int value)
		{
			if (!Validate(value)) return false;
			return !ex.Contains(value);
		}
	}
}
