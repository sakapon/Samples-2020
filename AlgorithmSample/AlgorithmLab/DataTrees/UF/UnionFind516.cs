using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.UF516
{
	// typed vertexes, data augmentation
	// 頂点を動的に追加できる方式
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TVertex, TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
		public class Group
		{
			public List<TVertex> Items;
			public int Count => Items.Count;
			public TValue Value;
		}

		public HashSet<Group> Groups { get; } = new HashSet<Group>();
		Dictionary<TVertex, Group> gMap = new Dictionary<TVertex, Group>();
		public int GroupsCount => Groups.Count;
		public int Count => gMap.Count;
		TValue iv;
		Func<TValue, TValue, TValue> mergeValues;

		public UnionFind(TValue iv, Func<TValue, TValue, TValue> mergeValues, IEnumerable<KeyValuePair<TVertex, TValue>> items = null)
		{
			this.iv = iv;
			this.mergeValues = mergeValues;
			if (items != null) foreach (var p in items) AddGroup(p.Key, p.Value);
		}

		void AddGroup(TVertex x, TValue value)
		{
			var g = new Group { Items = new List<TVertex> { x }, Value = value };
			Groups.Add(g);
			gMap[x] = g;
		}

		public bool Add(TVertex x, TValue value)
		{
			if (gMap.ContainsKey(x)) return false;
			AddGroup(x, value);
			return true;
		}

		public Group Find(TVertex x) => gMap.ContainsKey(x) ? gMap[x] : null;
		public bool AreSame(TVertex x, TVertex y) => gMap.Comparer.Equals(x, y) ||
			gMap.ContainsKey(x) && gMap.ContainsKey(y) && gMap[x] == gMap[y];

		public bool Union(TVertex x, TVertex y)
		{
			Add(x, iv);
			Add(y, iv);
			var (gx, gy) = (gMap[x], gMap[y]);
			if (gx == gy) return false;

			if (gx.Count < gy.Count) Merge(gy, gx); else Merge(gx, gy);
			return true;
		}

		void Merge(Group gx, Group gy)
		{
			Groups.Remove(gy);
			foreach (var v in gy.Items)
			{
				gx.Items.Add(v);
				gMap[v] = gx;
			}
			gx.Value = mergeValues(gx.Value, gy.Value);
		}
	}
}
