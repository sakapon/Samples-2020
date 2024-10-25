using System;
using System.Linq;

// Int32 vertexes, node-based
namespace AlgorithmLab.DataTrees.UF611
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}, {Value}\}")]
		public class Node
		{
			public int Item;
			public Node Parent;
			public int Size = 1;
			// 親を基準とした相対値
			public long Value;
		}

		readonly Node[] nodes;
		// path compression
		public Node this[int x] { get { Find(x); return nodes[x]; } }
		public int ItemsCount => nodes.Length;
		public int SetsCount { get; private set; }

		public UnionFind(int n)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Item = i };
			SetsCount = n;
		}

		Node Find(Node n)
		{
			if (n.Parent == null) return n;

			var r = Find(n.Parent);
			n.Value += n.Parent.Value;
			return n.Parent = r;
		}

		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => Find(x).Size;

		public bool Union(int x, int y, long x2y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			if (rx.Size < ry.Size)
			{
				(x, y) = (y, x);
				(rx, ry) = (ry, rx);
				x2y = -x2y;
			}
			ry.Parent = rx;
			rx.Size += ry.Size;
			--SetsCount;
			ry.Value = -nodes[y].Value + x2y + nodes[x].Value;
			return true;
		}

		public ILookup<Node, Node> ToSets() => nodes.ToLookup(Find);
	}
}
