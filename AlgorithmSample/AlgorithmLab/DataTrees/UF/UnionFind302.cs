using System;

namespace AlgorithmLab.DataTrees.UF302
{
	// union by rank, path compression
	// O(α(n))
	public class UnionFind
	{
		int[] parents, ranks;

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			ranks = new int[n];
		}

		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (ranks[x] < ranks[y]) Merge(y, x);
			else Merge(x, y);
			return true;
		}

		void Merge(int x, int y)
		{
			parents[y] = x;
			if (ranks[x] == ranks[y]) ++ranks[x];
		}
	}
}
