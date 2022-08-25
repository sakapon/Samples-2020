using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc237/tasks/abc237_d
// item: [0, n)
namespace AlgorithmLab.Collections.Linked.DistinctLinkedList201
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

		void Link(int pi, int ni)
		{
			prev[ni] = pi;
			next[pi] = ni;
		}

		public void AddFirst(int item)
		{
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			if (count == 0) li = item;
			else Link(item, fi);
			fi = item;
			++count;
		}
		public void AddLast(int item)
		{
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			if (count == 0) fi = item;
			else Link(li, item);
			li = item;
			++count;
		}

		public void AddBefore(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			var t = prev[target];
			if (t == -1) fi = item;
			else Link(t, item);
			Link(item, target);
			++count;
		}
		public void AddAfter(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			var t = next[target];
			if (t == -1) li = item;
			else Link(item, t);
			Link(target, item);
			++count;
		}

		public int RemoveFirst()
		{
			if (count == 0) throw new InvalidOperationException("No items are found.");

			var r = fi;
			if (--count == 0)
			{
				fi = li = -1;
			}
			else
			{
				fi = next[fi];
				prev[fi] = -1;
				next[r] = -1;
			}
			return r;
		}
		public int RemoveLast()
		{
			if (count == 0) throw new InvalidOperationException("No items are found.");

			var r = li;
			if (--count == 0)
			{
				fi = li = -1;
			}
			else
			{
				li = prev[li];
				next[li] = -1;
				prev[r] = -1;
			}
			return r;
		}

		public bool Remove(int item)
		{
			if (item != fi && prev[item] == -1 && next[item] == -1) return false;

			if (item == fi) RemoveFirst();
			else if (item == li) RemoveLast();
			else
			{
				--count;
				Link(prev[item], next[item]);
				prev[item] = next[item] = -1;
			}
			return true;
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
