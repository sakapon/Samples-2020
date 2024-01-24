using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHashMap<TKey, TValue> : IEnumerable<(TKey key, TValue value)>
	{
		List<TKey>[] keys;
		List<TValue>[] values;
		int n, f;
		readonly TValue iv;
		readonly IEqualityComparer<TKey> ec = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.Ordinal : EqualityComparer<TKey>.Default;

		public ListHashMap(TValue iv = default(TValue), int capacity = 8)
		{
			this.iv = iv;
			var c = 1;
			while (c < capacity) c <<= 1;
			Initialize(c);
		}

		public int Count => n;
		public void Clear() => Initialize(8);
		void Initialize(int c)
		{
			keys = new List<TKey>[c];
			values = new List<TValue>[c];
			n = 0;
			f = c - 1;
		}

		int GetIndex(List<TKey> l, TKey key)
		{
			if (l == null) return -1;
			for (int i = 0; i < l.Count; ++i)
				if (ec.Equals(l[i], key)) return i;
			return -1;
		}

		public TValue this[TKey key]
		{
			get
			{
				var h = key.GetHashCode() & f;
				var i = GetIndex(keys[h], key);
				return i != -1 ? values[h][i] : iv;
			}
			set
			{
				if (n == keys.Length >> 1) Expand();
				AddOrUpdate(key, value, true);
			}
		}

		public bool ContainsKey(TKey key)
		{
			var h = key.GetHashCode() & f;
			return GetIndex(keys[h], key) != -1;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var h = key.GetHashCode() & f;
			var i = GetIndex(keys[h], key);
			if (i != -1) { value = values[h][i]; return true; }
			else { value = iv; return false; }
		}

		public bool Add(TKey key, TValue value)
		{
			if (n == keys.Length >> 1) Expand();
			return AddOrUpdate(key, value, false);
		}

		bool AddOrUpdate(TKey key, TValue value, bool update)
		{
			var h = key.GetHashCode() & f;
			var l = keys[h];
			var i = GetIndex(l, key);
			if (i != -1)
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
			var i = GetIndex(l, key);
			if (i == -1) return false;
			l.RemoveAt(i);
			values[h].RemoveAt(i);
			--n;
			return true;
		}

		void Expand()
		{
			var (tk, tv) = (keys, values);
			Initialize(keys.Length << 1);
			for (int h = 0; h < tk.Length; ++h)
				if (tk[h] != null) for (int i = 0; i < tk[h].Count; ++i) AddOrUpdate(tk[h][i], tv[h][i], false);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<(TKey key, TValue value)> GetEnumerator()
		{
			for (int h = 0; h < keys.Length; ++h)
				if (keys[h] != null) for (int i = 0; i < keys[h].Count; ++i) yield return (keys[h][i], values[h][i]);
		}
	}
}
