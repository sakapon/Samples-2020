using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.UF416
{
	// typed vertexes
	// 頂点を動的に追加できる方式
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
	public class QuickFind<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
		public class Group
		{
			public List<T> Items;
			public int Count => Items.Count;
		}

		public HashSet<Group> Groups { get; } = new HashSet<Group>();
		Dictionary<T, Group> gMap = new Dictionary<T, Group>();
		public int GroupsCount => Groups.Count;
		public int Count => gMap.Count;

		public QuickFind(IEnumerable<T> items = null)
		{
			if (items != null) foreach (var x in items) AddGroup(x);
		}

		void AddGroup(T x)
		{
			var g = new Group { Items = new List<T> { x } };
			Groups.Add(g);
			gMap[x] = g;
		}

		public bool Add(T x)
		{
			if (gMap.ContainsKey(x)) return false;
			AddGroup(x);
			return true;
		}

		public Group Find(T x) => gMap.ContainsKey(x) ? gMap[x] : null;
		public bool AreSame(T x, T y) => gMap.Comparer.Equals(x, y) ||
			gMap.ContainsKey(x) && gMap.ContainsKey(y) && gMap[x] == gMap[y];

		public bool Union(T x, T y)
		{
			Add(x);
			Add(y);
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
		}
	}
}
