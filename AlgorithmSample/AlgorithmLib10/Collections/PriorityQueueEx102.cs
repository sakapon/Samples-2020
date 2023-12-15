// 実用不可能な実装です。
// A priority queue that can remove items
namespace AlgorithmLib10.Collections.PriorityQueueEx102
{
	public class PriorityQueueEx<T> : PriorityQueue<T, T> where T : notnull
	{
		readonly Dictionary<T, int> removed = new();

		public PriorityQueueEx() { }
		public PriorityQueueEx(IEnumerable<T> items) : base(items.Select(v => (v, v))) { }

		public T First => Peek();

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

		// キューに存在するかどうかを判定できません。
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
				base.Dequeue();
			}
		}
	}
}
