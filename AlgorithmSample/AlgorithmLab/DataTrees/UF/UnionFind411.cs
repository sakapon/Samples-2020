using System;
using System.Linq;

// 実用可能です。
// Int32 vertexes, node-based
namespace AlgorithmLab.DataTrees.UF411
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		public class Node
		{
			public int Item;
			public Node Parent;
			public int Size = 1;
		}

		readonly Node[] nodes;
		// path compression
		public Node this[int x] { get { Find(x); return nodes[x]; } }
		public int ItemsCount => nodes.Length;
		public int SetsCount { get; private set; }

		// (parent, child)
		public event Action<int, int> United;

		public UnionFind(int n)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Item = i };
			SetsCount = n;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => Find(x).Size;

		public bool Union(int x, int y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			--SetsCount;
			United?.Invoke(rx.Item, ry.Item);
			return true;
		}

		public ILookup<Node, Node> ToSets() => nodes.ToLookup(Find);
	}
}
