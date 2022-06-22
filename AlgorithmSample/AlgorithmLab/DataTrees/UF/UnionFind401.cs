﻿using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF401
{
	// Int32 vertexes
	public class UnionFind
	{
		int[] parents, sizes;
		public int GroupsCount { get; private set; }

		public UnionFind(int n)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			GroupsCount = n;
		}

		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => sizes[Find(x)];

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			if (sizes[x] < sizes[y]) Merge(y, x);
			else Merge(x, y);
			return true;
		}

		void Merge(int x, int y)
		{
			parents[y] = x;
			sizes[x] += sizes[y];
			--GroupsCount;
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
