using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashMap<TKey, TValue> : IEnumerable<(TKey key, TValue value)>
	{
		int n, f;
		int[] firsts, nexts;
		bool[] u;
		TKey[] keys;
		TValue[] values;
		readonly TValue iv;
		readonly IEqualityComparer<TKey> ec = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.Ordinal : EqualityComparer<TKey>.Default;

		public ArrayHashMap(TValue iv = default(TValue), int capacity = 8)
		{
			this.iv = iv;
			var c = 1;
			while (c < capacity) c <<= 1;
			Initialize(c);
		}

		void Initialize(int c)
		{
			n = 0;
			f = c - 1;
			firsts = new int[c];
			nexts = new int[c];
			u = new bool[c];
			keys = new TKey[c];
			values = new TValue[c];
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
		}

		public int Count => n;

		public void Clear() => Initialize(8);

		public TValue this[TKey key]
		{
			get
			{
				var i = GetIndex(key);
				return i != -1 ? values[i] : iv;
			}
			set
			{
				if (n == keys.Length >> 1) Expand();
				AddOrUpdate(key, value, true);
			}
		}

		public bool ContainsKey(TKey key) => GetIndex(key) != -1;

		public bool TryGetValue(TKey key, out TValue value)
		{
			var i = GetIndex(key);
			if (i != -1) { value = values[i]; return true; }
			else { value = iv; return false; }
		}

		public bool Add(TKey key, TValue value)
		{
			if (n == keys.Length >> 1) Expand();
			return AddOrUpdate(key, value, false);
		}

		public bool Remove(TKey key)
		{
			var h = key.GetHashCode() & f;
			var last = -1;
			for (var i = firsts[h]; i != -1; last = i, i = nexts[i])
			{
				if (!ec.Equals(keys[i], key)) continue;

				--n;
				if (last == -1) firsts[h] = nexts[i];
				else nexts[last] = nexts[i];
				nexts[i] = -1;
				u[i] = false;
				return true;
			}
			return false;
		}

		int GetIndex(TKey key)
		{
			var h = key.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(keys[i], key)) return i;
			return -1;
		}

		bool AddOrUpdate(TKey key, TValue value, bool update)
		{
			var h = key.GetHashCode() & f;
			var last = -1;
			var i = firsts[h];
			for (; i != -1; last = i, i = nexts[i])
				if (ec.Equals(keys[i], key))
				{
					if (update) values[i] = value;
					return update;
				}

			i = last == -1 ? h : (last + 1) & f;
			while (u[i]) i = (i + 1) & f;

			++n;
			if (last == -1) firsts[h] = i;
			else nexts[last] = i;
			u[i] = true;
			keys[i] = key;
			values[i] = value;
			return true;
		}

		void Expand()
		{
			var (tu, tk, tv) = (u, keys, values);
			Initialize(u.Length << 1);
			for (var i = 0; i < tu.Length; ++i) if (tu[i]) AddOrUpdate(tk[i], tv[i], false);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<(TKey key, TValue value)> GetEnumerator() { for (var i = 0; i <= f; ++i) if (u[i]) yield return (keys[i], values[i]); }
	}
}
