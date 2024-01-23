using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays101
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
		readonly IEqualityComparer<TKey> ec = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.Ordinal : EqualityComparer<TKey>.Default;
		readonly TValue iv;

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
			get => TryGetValue(key, out var value) ? value : value;
			set => AddOrUpdate(key, value, true);
		}

		public bool ContainsKey(TKey key)
		{
			var h = key.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(keys[i], key)) return true;
			return false;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var h = key.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(keys[i], key))
				{
					value = values[i];
					return true;
				}
			value = iv;
			return false;
		}

		public bool Add(TKey key, TValue value)
		{
			return AddOrUpdate(key, value, false);
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

		IEnumerable<int> GetAddresses(int h)
		{
			for (var i = firsts[h]; i != -1; i = nexts[i])
				yield return i;
		}
	}
}
