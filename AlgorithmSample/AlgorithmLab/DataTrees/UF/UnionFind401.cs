using System;
using System.Linq;

// 実用可能です。
// Int32 vertexes
namespace AlgorithmLab.DataTrees.UF401
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind
	{
		readonly int[] parents, sizes;
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

		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => sizes[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (sizes[x] < sizes[y]) (x, y) = (y, x);
			parents[y] = x;
			sizes[x] += sizes[y];
			--SetsCount;
			return true;
		}

		public ILookup<int, int> ToSets() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
