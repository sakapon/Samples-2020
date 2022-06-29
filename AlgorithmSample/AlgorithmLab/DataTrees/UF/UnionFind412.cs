using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.UF412
{
	// Int32 vertexes
	public class UnionFind
	{
		List<int>[] gMap;
		public HashSet<List<int>> Groups { get; }
		public int GroupsCount => Groups.Count;

		public UnionFind(int n)
		{
			gMap = new List<int>[n];
			for (int i = 0; i < n; ++i) gMap[i] = new List<int> { i };
			Groups = new HashSet<List<int>>(gMap);
		}

		public List<int> Find(int x) => gMap[x];
		public bool AreSame(int x, int y) => gMap[x] == gMap[y];
		public int GetSize(int x) => gMap[x].Count;

		public bool Union(int x, int y)
		{
			if (gMap[x] == gMap[y]) return false;

			if (gMap[x].Count < gMap[y].Count) Merge(y, x);
			else Merge(x, y);
			return true;
		}

		void Merge(int x, int y)
		{
			Groups.Remove(gMap[y]);
			var gx = gMap[x];
			foreach (var v in gMap[y])
			{
				gx.Add(v);
				gMap[v] = gx;
			}
		}
	}
}
