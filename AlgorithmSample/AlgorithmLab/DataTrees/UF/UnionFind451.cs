using System;
using System.Collections.Generic;
using System.Linq;

// union by size, undo
// O(log n)
namespace AlgorithmLab.DataTrees.UF451
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind
	{
		readonly int[] parents, sizes;
		readonly Stack<(int rx, int ry)> history = new Stack<(int rx, int ry)>();
		public int ItemsCount => parents.Length;
		public int SetsCount { get; private set; }

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			SetsCount = n;
		}

		public int Find(int x) => parents[x] == -1 ? x : Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => sizes[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y)))
			{
				history.Push((x, y));
				return false;
			}

			if (sizes[x] < sizes[y]) (x, y) = (y, x);
			parents[y] = x;
			sizes[x] += sizes[y];
			--SetsCount;
			history.Push((x, y));
			return true;
		}

		// Union メソッドの呼び出しに対応するため、分離されるとは限りません。
		public bool Undo(out int rx, out int ry)
		{
			if (history.Count == 0) throw new InvalidOperationException("No history.");

			(rx, ry) = history.Pop();
			if (rx == ry) return false;

			parents[ry] = -1;
			sizes[rx] -= sizes[ry];
			++SetsCount;
			return true;
		}

		public ILookup<int, int> ToSets() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
