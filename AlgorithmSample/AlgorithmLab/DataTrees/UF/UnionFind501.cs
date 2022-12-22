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

		public UnionFind(int n, Func<TValue, TValue, TValue> mergeValues, TValue[] values = null)
		{
			parents = new int[n];
			Array.Fill(parents, -1);
			sizes = new int[n];
			Array.Fill(sizes, 1);
			groupsCount = n;
			this.mergeValues = mergeValues;
			this.values = new TValue[n];
			if (values != null) Array.Copy(values, this.values, n);
		}

		public int ItemsCount => parents.Length;
		public int GroupsCount => groupsCount;
		public int Find(int x) => parents[x] == -1 ? x : parents[x] = Find(parents[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => sizes[Find(x)];

		public TValue this[int x]
		{
			get => values[Find(x)];
			set => values[Find(x)] = value;
		}

		public bool Union(int x, int y)
		{
			if ((x = Find(x)) == (y = Find(y))) return false;

			// 左右の順序を保って値をマージします。
			var v = mergeValues(values[x], values[y]);

			if (sizes[x] < sizes[y]) (x, y) = (y, x);
			parents[y] = x;
			sizes[x] += sizes[y];
			--groupsCount;
			values[x] = v;
			return true;
		}

		public ILookup<int, int> ToGroups() => Enumerable.Range(0, parents.Length).ToLookup(Find);
	}
}
