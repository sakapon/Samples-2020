using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc237/tasks/abc237_d
// item: [0, n)
// With a unique anchor.
namespace AlgorithmLab.Collections.Linked.DistinctLinkedList202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class DistinctLinkedList : IEnumerable<int>
	{
		const int Invalid = -1;
		readonly int n;
		readonly int[] prev;
		readonly int[] next;
		int count;

		public DistinctLinkedList(int size)
		{
			n = size;
			prev = new int[n + 1];
			next = new int[n + 1];
			Array.Fill(prev, Invalid);
			Array.Fill(next, Invalid);
			prev[n] = next[n] = n;
		}

		public int Size => n;
		public int Count => count;
		public int First => next[n];
		public int Last => prev[n];

		public bool Contains(int item) => next[item] != Invalid;

		void Link(int pi, int ni)
		{
			prev[ni] = pi;
			next[pi] = ni;
		}

		public void AddFirst(int item) => AddAfter(n, item);
		public void AddLast(int item) => AddBefore(n, item);

		public void AddBefore(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			Link(prev[target], item);
			Link(item, target);
			++count;
		}
		public void AddAfter(int target, int item)
		{
			if (!Contains(target)) throw new ArgumentException("The target is not found.", nameof(target));
			if (Contains(item)) throw new ArgumentException("The item is found.", nameof(item));

			Link(item, next[target]);
			Link(target, item);
			++count;
		}

		public int RemoveFirst()
		{
			if (count == 0) throw new InvalidOperationException("No items are found.");

			var r = next[n];
			Remove(r);
			return r;
		}
		public int RemoveLast()
		{
			if (count == 0) throw new InvalidOperationException("No items are found.");

			var r = prev[n];
			Remove(r);
			return r;
		}

		public bool Remove(int item)
		{
			if (!Contains(item)) return false;

			Link(prev[item], next[item]);
			prev[item] = next[item] = Invalid;
			--count;
			return true;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator() { for (var v = next[n]; v != n; v = next[v]) yield return v; }

		public int[] ToArray()
		{
			var r = new int[count];
			for (int i = 0, v = next[n]; v != n; ++i, v = next[v]) r[i] = v;
			return r;
		}
	}
}
