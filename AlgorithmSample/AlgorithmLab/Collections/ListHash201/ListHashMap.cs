using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash201
{
	public class ListHashMap<TKey, TValue>
	{
		readonly List<TKey>[] keys;
		readonly List<TValue>[] values;
		readonly int f;
		int n;

		public ListHashMap(int capacity)
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

		public TValue this[TKey key]
		{
			get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
			set => AddOrUpdate(key, value, true);
		}

		public bool ContainsKey(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			return l != null && l.Contains(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			var i = -1;
			var b = l != null && (i = l.IndexOf(key)) != -1;
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
			if (l != null && (i = l.IndexOf(key)) != -1)
			{
				if (update) values[h][i] = value;
				return update;
			}

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

		public bool Remove(TKey key)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			int i;
			if (l == null || (i = l.IndexOf(key)) == -1) return false;
			l.RemoveAt(i);
			values[h].RemoveAt(i);
			--n;
			return true;
		}
	}
}
