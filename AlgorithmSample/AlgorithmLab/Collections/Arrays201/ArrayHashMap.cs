using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays201
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashMap<TKey, TValue>
	{
		int n;
		readonly int f;
		readonly int[] firsts;
		readonly int[] nexts;
		readonly bool[] u;
		readonly TKey[] keys;
		readonly TValue[] values;
		readonly TValue iv;
		readonly IEqualityComparer<TKey> ec = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.Ordinal : EqualityComparer<TKey>.Default;

		public ArrayHashMap(int capacity, TValue iv = default(TValue))
		{
			f = capacity - 1;
			firsts = new int[capacity];
			nexts = new int[capacity];
			u = new bool[capacity];
			keys = new TKey[capacity];
			values = new TValue[capacity];
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
			this.iv = iv;
		}

		public int Count => n;

		public void Clear()
		{
			n = 0;
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
			Array.Clear(u, 0, u.Length);
		}

		public TValue this[TKey key]
		{
			get
			{
				var i = GetIndex(key);
				return i != -1 ? values[i] : iv;
			}
			set => AddOrUpdate(key, value, true);
		}

		public bool ContainsKey(TKey key) => GetIndex(key) != -1;

		public bool TryGetValue(TKey key, out TValue value)
		{
			var i = GetIndex(key);
			if (i != -1) { value = values[i]; return true; }
			else { value = iv; return false; }
		}

		public bool Add(TKey key, TValue value) => AddOrUpdate(key, value, false);

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

		IEnumerable<int> GetAddresses(int h)
		{
			for (var i = firsts[h]; i != -1; i = nexts[i])
				yield return i;
		}
	}
}
