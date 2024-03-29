﻿using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.UF111
{
	// O(log n)
	// Find 操作が多い場合に有利
	public class QuickFind
	{
		List<int>[] groups;

		public QuickFind(int n)
		{
			groups = new List<int>[n];
			for (int i = 0; i < n; ++i) groups[i] = new List<int> { i };
		}

		public List<int> Find(int x) => groups[x];
		public bool AreSame(int x, int y) => groups[x] == groups[y];
		public int GetSize(int x) => groups[x].Count;

		public bool Union(int x, int y)
		{
			if (groups[x] == groups[y]) return false;

			if (groups[x].Count < groups[y].Count) Merge(y, x);
			else Merge(x, y);
			return true;
		}

		void Merge(int x, int y)
		{
			var gx = groups[x];
			foreach (var v in groups[y])
			{
				gx.Add(v);
				groups[v] = gx;
			}
		}
	}
}
