using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF501
{
	// Int32 vertexes
	public class UnionFind<TValue>
	{
		int[] parents, sizes;
		public int GroupsCount { get; private set; }
		TValue[] values;
		Func<TValue, TValue, TValue> mergeValues;

		public UnionFind(int n, TValue[] values, Func<TValue, TValue, TValue> mergeValues)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			GroupsCount = n;
			this.values = values;
			this.mergeValues = mergeValues;
		}

		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => sizes[Find(x)];
		public TValue GetValue(int x) => values[Find(x)];

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
			values[x] = mergeValues(values[x], values[y]);
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
