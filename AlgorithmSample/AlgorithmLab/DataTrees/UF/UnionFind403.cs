using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF403
{
	// typed vertexes
	// 頂点を静的に登録する方式
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
	public class UnionFind<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		public class Node
		{
			public T Item;
			public Node Parent;
			public int Size = 1;
		}

		Dictionary<T, Node> nodes = new Dictionary<T, Node>();
		public int Count => nodes.Count;
		public int GroupsCount { get; private set; }

		public UnionFind(IEnumerable<T> collection)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			foreach (var x in collection) nodes[x] = new Node { Item = x };
			GroupsCount = nodes.Count;
		}

		public Node Find(T x) => nodes.ContainsKey(x) ? Find(nodes[x]) : throw new InvalidOperationException("The specified node is not found.");
		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public bool AreSame(T x, T y) => Find(x) == Find(y);
		public int GetSize(T x) => Find(x).Size;

		public bool Union(T x, T y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			if (nx.Size < ny.Size) Merge(ny, nx);
			else Merge(nx, ny);
			return true;
		}

		void Merge(Node nx, Node ny)
		{
			ny.Parent = nx;
			nx.Size += ny.Size;
			--GroupsCount;
		}

		public ILookup<Node, Node> ToGroups() => nodes.Values.ToLookup(Find);
	}
}
