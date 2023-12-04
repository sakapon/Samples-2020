namespace AlgorithmLib10.Collections
{
	public class PriorityQueueEx<T> : PriorityQueue<T, T> where T : notnull
	{
		readonly Dictionary<T, int> removed = new();

		public PriorityQueueEx() { }
		public PriorityQueueEx(IEnumerable<T> items) : base(items.Select(v => (v, v))) { }

		public void Enqueue(T item)
		{
			base.Enqueue(item, item);
		}

		public new T Dequeue()
		{
			var r = base.Dequeue();
			EnsureFirst();
			return r;
		}

		public void Remove(T item)
		{
			removed[item] = removed.GetValueOrDefault(item) + 1;
			EnsureFirst();
		}

		void EnsureFirst()
		{
			while (Count > 0)
			{
				var item = Peek();
				if (!removed.TryGetValue(item, out var c)) break;
				if (--c == 0) removed.Remove(item);
				else removed[item] = c;
				Dequeue();
			}
		}
	}
}
