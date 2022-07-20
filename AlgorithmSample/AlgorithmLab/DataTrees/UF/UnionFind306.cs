using System;

namespace AlgorithmLab.DataTrees.UF306
{
	// union by size, path compression
	// parents[root] = -size
	// O(α(n))
	public class UnionFind
	{
		int[] parents;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
		}

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
		}
	}
}
