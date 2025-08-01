using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.HashTables.v101
{
	public class IntArrayMap<TValue>
	{
		readonly bool[] b;
		readonly TValue[] values;
		public int Count { get; private set; }

		public IntArrayMap(int n)
		{
			b = new bool[n];
			values = new TValue[n];
		}

		public TValue this[int key]
		{
			get => b[key] ? values[key] : throw new KeyNotFoundException();
			set
			{
				if (!b[key]) ++Count;
				b[key] = true;
				values[key] = value;
			}
		}

		public void Clear()
		{
			Array.Clear(b, 0, b.Length);
			Count = 0;
		}

		public bool ContainsKey(int key)
		{
			return b[key];
		}

		public bool Add(int key, TValue value)
		{
			if (b[key]) return false;
			b[key] = true;
			values[key] = value;
			++Count;
			return true;
		}

		public bool Remove(int key)
		{
			if (!b[key]) return false;
			b[key] = false;
			--Count;
			return true;
		}
	}

	public class IntArraySet
	{
		readonly IntArrayMap<bool> map;
		public IntArraySet(int n) => map = new IntArrayMap<bool>(n);
		public int Count => map.Count;
		public void Clear() => map.Clear();
		public bool Contains(int item) => map.ContainsKey(item);
		public bool Add(int item) => map.Add(item, false);
		public bool Remove(int item) => map.Remove(item);
	}
}
