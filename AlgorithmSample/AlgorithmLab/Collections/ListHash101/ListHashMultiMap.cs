using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash101
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
			if (l == null) return 0;

			var c = 0;
			for (int i = l.Count - 1; i >= 0; --i)
			{
				if (ec.Equals(l[i], key))
				{
					l.RemoveAt(i);
					values[h].RemoveAt(i);
					--n;
					++c;
				}
			}
			return c;
		}
	}
}
