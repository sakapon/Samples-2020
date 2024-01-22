using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash101
{
	public class ListHashMap<TKey, TValue>
	{
		List<TKey>[] keys;
		List<TValue>[] values;
		readonly int f;
		int n;

		public ListHashMap(int capacity)
		{
			keys = Array.ConvertAll(new bool[capacity], _ => new List<TKey>());
			values = Array.ConvertAll(new bool[capacity], _ => new List<TValue>());
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			keys = Array.ConvertAll(keys, _ => new List<TKey>());
			values = Array.ConvertAll(values, _ => new List<TValue>());
			n = 0;
		}

		public TValue this[TKey key]
		{
			get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
			set => AddOrUpdate(key, value, true);
		}

		public bool ContainsKey(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			return l.Contains(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			int i;
			var b = (i = l.IndexOf(key)) != -1;
			value = b ? values[h][i] : default(TValue);
			return b;
		}

		public bool Add(TKey key, TValue value)
		{
			return AddOrUpdate(key, value, false);
		}

		bool AddOrUpdate(TKey key, TValue value, bool update)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			int i;
			if ((i = l.IndexOf(key)) != -1)
			{
				if (update) values[h][i] = value;
				return update;
			}

			l.Add(key);
			values[h].Add(value);
			++n;
			return true;
		}

		public bool Remove(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			int i;
			if ((i = l.IndexOf(key)) == -1) return false;
			l.RemoveAt(i);
			values[h].RemoveAt(i);
			--n;
			return true;
		}
	}
}
