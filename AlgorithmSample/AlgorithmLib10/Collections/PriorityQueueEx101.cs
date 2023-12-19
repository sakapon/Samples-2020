// A priority queue that can remove items
namespace AlgorithmLib10.Collections.PriorityQueueEx101
{
	public class PriorityQueueEx<T> : PriorityQueue<T, T> where T : notnull
	{
		readonly Dictionary<T, int> counts = new();

		public PriorityQueueEx() { }
		public PriorityQueueEx(IEnumerable<T> items)
		{
			if (items != null) foreach (var v in items) Enqueue(v);
		}

		public T First => Peek();

		public void Enqueue(T item)
		{
			base.Enqueue(item, item);
			counts[item] = counts.GetValueOrDefault(item) + 1;
		}

		public new T Dequeue()
		{
			var r = base.Dequeue();
			EnsureFirst();
			return r;
		}

		public bool Remove(T item)
		{
			if (!counts.TryGetValue(item, out var c)) return false;
			if (--c == 0) counts.Remove(item);
			else counts[item] = c;
			EnsureFirst();
			return true;
		}

		void EnsureFirst()
		{
			while (Count > 0 && !counts.ContainsKey(Peek())) base.Dequeue();
		}
	}
}
