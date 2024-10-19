using System;
using System.Linq;

// path compression
// O(log n)
namespace AlgorithmLab.DataTrees.UF202
{
	public class UnionFind
	{
		readonly int[] p;
		public UnionFind(int n) => p = Enumerable.Repeat(-1, n).ToArray();
		public int Find(int x) => p[x] == -1 ? x : p[x] = Find(p[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public void Union(int x, int y) { if ((x = Find(x)) != (y = Find(y))) p[y] = x; }
	}
}
