using System;

namespace AlgorithmLab.Collections.HashTables
{
	public class IntArraySet
	{
		readonly bool[] a;
		int c;

		public IntArraySet(int size)
		{
			a = new bool[size];
		}

		public int Count => c;
		public void Clear()
		{
			Array.Clear(a, 0, a.Length);
			c = 0;
		}

		public bool Contains(int item)
		{
			return a[item];
		}

		public bool Add(int item)
		{
			if (a[item]) return false;
			++c;
			a[item] = true;
			return true;
		}

		public bool Remove(int item)
		{
			if (!a[item]) return false;
			--c;
			a[item] = false;
			return true;
		}
	}
}
