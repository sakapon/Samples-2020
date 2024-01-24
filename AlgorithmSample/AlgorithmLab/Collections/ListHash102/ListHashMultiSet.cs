using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.ListHash102
{
	public class ListHashMultiSet<T>
	{
		readonly List<T>[] t;
		readonly List<int>[] counts;
		readonly int f;
		int n;

		public ListHashMultiSet(int capacity)
		{
			t = new List<T>[capacity];
			counts = new List<int>[capacity];
			f = capacity - 1;
		}

		public int Count => n;

		public void Clear()
		{
			Array.Clear(t, 0, t.Length);
			Array.Clear(counts, 0, counts.Length);
			n = 0;
		}

		public int GetCount(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			int i;
			return l != null && (i = l.IndexOf(item)) != -1 ? counts[h][i] : 0;
		}

		public bool Add(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			int i;
			if (l != null && (i = l.IndexOf(item)) != -1)
			{
				++counts[h][i];
				return true;
			}

			if (l == null)
			{
				t[h] = new List<T> { item };
				counts[h] = new List<int> { 1 };
			}
			else
			{
				l.Add(item);
				counts[h].Add(1);
			}
			++n;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var l = t[h];
			int i;
			if (l == null || (i = l.IndexOf(item)) == -1) return false;

			if (counts[h][i] == 1)
			{
				l.RemoveAt(i);
				counts[h].RemoveAt(i);
			}
			else
			{
				--counts[h][i];
			}
			--n;
			return true;
		}
	}
}
