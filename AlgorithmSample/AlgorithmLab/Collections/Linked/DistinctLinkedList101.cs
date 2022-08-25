using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc237/tasks/abc237_d
// item: [0, n)
// Add only.
namespace AlgorithmLab.Collections.Linked.DistinctLinkedList101
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class DistinctLinkedList : IEnumerable<int>
	{
		readonly int n;
		readonly int[] prev;
		readonly int[] next;
		int count, fi = -1, li = -1;

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
		public int First => fi;
		public int Last => li;

		public bool Contains(int item) => count <= 1 ? fi == item : (prev[item] != -1 || next[item] != -1);

		public void AddFirst(int item)
		{
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			if (count == 0)
			{
				li = item;
			}
			else
			{
				prev[fi] = item;
				next[item] = fi;
			}
			fi = item;
			++count;
		}
		public void AddLast(int item)
		{
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			if (count == 0)
			{
				fi = item;
			}
			else
			{
				next[li] = item;
				prev[item] = li;
			}
			li = item;
			++count;
		}

		public void AddBefore(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			var t = prev[target];
			if (t == -1)
			{
				fi = item;
			}
			else
			{
				prev[item] = t;
				next[t] = item;
			}
			prev[target] = item;
			next[item] = target;
			++count;
		}
		public void AddAfter(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			var t = next[target];
			if (t == -1)
			{
				li = item;
			}
			else
			{
				next[item] = t;
				prev[t] = item;
			}
			next[target] = item;
			prev[item] = target;
			++count;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator() { for (var v = fi; v != -1; v = next[v]) yield return v; }

		public int[] ToArray()
		{
			var r = new int[count];
			for (int i = 0, v = fi; v != -1; ++i, v = next[v]) r[i] = v;
			return r;
		}
	}
}
