using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF406
{
	// Int32 vertexes
	// parents[root] = -size
	public class UnionFind
	{
		readonly int[] parents;
		int groupsCount;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			groupsCount = n;
		}

		public int ItemsCount => parents.Length;
		public int GroupsCount => groupsCount;
		public int Find(int x) => parents[x] < 0 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => -parents[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (parents[x] > parents[y]) Merge(y, x);
			else Merge(x, y);
			return true;
		}

		void Merge(int x, int y)
		{
			parents[x] += parents[y];
			parents[y] = x;
			--groupsCount;
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
