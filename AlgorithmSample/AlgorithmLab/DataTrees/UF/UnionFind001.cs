﻿using System;

namespace AlgorithmLab.DataTrees.UF
{
	// O(n)
	public class UnionFind001
	{
		int[] parents;

		public UnionFind001(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
		}

		public int Find(int x) => parents[x] == -1 ? x : Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			parents[y] = x;
			return true;
		}
	}
}
