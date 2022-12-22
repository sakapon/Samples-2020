using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF502
{
	// typed keys, data augmentation
	// 頂点を動的に追加できる方式
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TKey, TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
		public class Node
		{
			public TKey Key;
			public TValue Value;
			public Node Parent;
			public int Size = 1;
		}

		readonly Dictionary<TKey, Node> nodes = new Dictionary<TKey, Node>();
		public int Count => nodes.Count;
		public int GroupsCount { get; private set; }
		readonly Func<TValue, TValue, TValue> mergeValues;

		public UnionFind(Func<TValue, TValue, TValue> mergeValues, IEnumerable<KeyValuePair<TKey, TValue>> collection = null)
		{
			if (collection != null)
				foreach (var p in collection) nodes[p.Key] = new Node { Key = p.Key, Value = p.Value };
			GroupsCount = nodes.Count;
			this.mergeValues = mergeValues;
		}

		public bool ContainsKey(TKey x) => nodes.ContainsKey(x);
		public Node Add(TKey x, TValue value)
		{
			if (nodes.ContainsKey(x)) return null;
			++GroupsCount;
			return nodes[x] = new Node { Key = x, Value = value };
		}

		public Node Find(TKey x) => nodes.TryGetValue(x, out var n) ? Find(n) : null;
		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);

		public bool AreSame(TKey x, TKey y)
		{
			if (nodes.Comparer.Equals(x, y)) return true;
			var nx = Find(x);
			var ny = Find(y);
			return nx != null && nx == ny;
		}
		public int GetSize(TKey x) => Find(x)?.Size ?? 0;

		public TValue this[TKey x]
		{
			get
			{
				var n = Find(x) ?? throw new KeyNotFoundException();
				return n.Value;
			}
			set
			{
				var n = Find(x) ?? throw new KeyNotFoundException();
				if (n == null) Add(x, value);
				else n.Value = value;
			}
		}

		public bool Union(TKey x, TKey y)
		{
			var nx = Find(x) ?? throw new KeyNotFoundException();
			var ny = Find(y) ?? throw new KeyNotFoundException();
			if (nx == ny) return false;

			// 左右の順序を保って値をマージします。
			var v = mergeValues(nx.Value, ny.Value);

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
			nx.Value = v;
			return true;
		}

		public ILookup<Node, Node> ToGroups() => nodes.Values.ToLookup(Find);
	}
}
