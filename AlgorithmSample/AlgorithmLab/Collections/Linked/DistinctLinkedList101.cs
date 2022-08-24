using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc237/tasks/abc237_d
namespace AlgorithmLab.Collections.Linked.DistinctLinkedList101
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class DistinctLinkedList : IEnumerable<int>
	{
		readonly int n;
		int count, fv = -1, lv = -1;
		readonly int[] prev;
		readonly int[] next;

		public DistinctLinkedList(int size)
		{
			n = size;
			prev = new int[n];
			next = new int[n];
			Array.Fill(prev, -1);
			Array.Fill(next, -1);
		}

		public int Size => n;
		public int Count => count;
		public int First => fv;
		public int Last => lv;

		public bool Contains(int value) => count <= 1 ? fv == value : (prev[value] != -1 || next[value] != -1);

		public void AddFirst(int value)
		{
			if (Contains(value)) throw new ArgumentException("The value is found.", nameof(value));

			if (count == 0)
			{
				lv = value;
			}
			else
			{
				prev[fv] = value;
				next[value] = fv;
			}
			fv = value;
			++count;
		}
		public void AddLast(int value)
		{
			if (Contains(value)) throw new ArgumentException("The value is found.", nameof(value));

			if (count == 0)
			{
				fv = value;
			}
			else
			{
				next[lv] = value;
				prev[value] = lv;
			}
			lv = value;
			++count;
		}

		public void AddBefore(int target, int value)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(value)) throw new ArgumentException("The value is found.", nameof(value));

			var pv = prev[target];
			if (pv == -1)
			{
				fv = value;
			}
			else
			{
				prev[value] = pv;
				next[pv] = value;
			}
			prev[target] = value;
			next[value] = target;
			++count;
		}
		public void AddAfter(int target, int value)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(value)) throw new ArgumentException("The value is found.", nameof(value));

			var nv = next[target];
			if (nv == -1)
			{
				lv = value;
			}
			else
			{
				next[value] = nv;
				prev[nv] = value;
			}
			next[target] = value;
			prev[value] = target;
			++count;
		}

		//public bool Remove(int value);
		//public int RemoveFirst();
		//public int RemoveLast();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator() { for (var v = fv; v != -1; v = next[v]) yield return v; }

		public int[] ToArray()
		{
			var r = new int[count];
			for (int i = 0, v = fv; v != -1; ++i, v = next[v]) r[i] = v;
			return r;
		}
	}
}
