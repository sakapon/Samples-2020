using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF501
{
	// Int32 vertexes, data augmentation
	public class UnionFind<TValue>
	{
		readonly int[] parents, sizes;
		int groupsCount;
		readonly Func<TValue, TValue, TValue> mergeValues;
		readonly TValue[] values;

		public UnionFind(int n, Func<TValue, TValue, TValue> mergeValues, TValue[] values)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			groupsCount = n;
			this.mergeValues = mergeValues;
			this.values = new TValue[n];
			Array.Copy(values, this.values, n);
		}

		public int ItemsCount => parents.Length;
		public int GroupsCount => groupsCount;
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
			--groupsCount;
			values[x] = mergeValues(values[x], values[y]);
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
