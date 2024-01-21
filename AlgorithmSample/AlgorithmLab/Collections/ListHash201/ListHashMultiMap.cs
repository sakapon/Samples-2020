using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash201
{
	public class ListHashMultiMap<TKey, TValue>
	{
		readonly List<TKey>[] keys;
		readonly List<TValue>[] values;
		readonly int f;
		int n;
		readonly EqualityComparer<TKey> ec = EqualityComparer<TKey>.Default;

		public ListHashMultiMap(int capacity)
		{
			keys = new List<TKey>[capacity];
			values = new List<TValue>[capacity];
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			Array.Clear(keys, 0, keys.Length);
			Array.Clear(values, 0, values.Length);
			n = 0;
		}

		public IEnumerable<TValue> this[TKey key]
		{
			get
			{
				var h = key.GetHashCode() & f;
				var l = keys[h];
				if (l == null) yield break;
				for (int i = 0; i < l.Count; ++i)
					if (ec.Equals(l[i], key)) yield return values[h][i];
			}
		}

		public bool ContainsKey(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			return l != null && l.Contains(key);
		}

		public bool Add(TKey key, TValue value)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];

			if (l == null)
			{
				keys[h] = new List<TKey> { key };
				values[h] = new List<TValue> { value };
			}
			else
			{
				l.Add(key);
				values[h].Add(value);
			}
			++n;
			return true;
		}

		public int RemoveAll(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			if (l == null || l.Count == 0) return 0;

			var tk = new List<TKey>();
			var tv = new List<TValue>();

			for (int i = 0; i < l.Count; ++i)
			{
				if (!ec.Equals(l[i], key))
				{
					tk.Add(l[i]);
					tv.Add(values[h][i]);
				}
			}

			var c = tk.Count - l.Count;
			n -= c;
			keys[h] = tk;
			values[h] = tv;
			return c;
		}
	}
}
