using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF602
{
	// Int32 vertexes
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Key}, {Value}\}")]
		public class Node
		{
			public int Key;
			public TValue Value;
			public Node Parent;
			public int Size = 1;
		}

		readonly Node[] nodes;
		readonly Func<TValue, TValue, TValue> composition;
		readonly Func<TValue, TValue> inverse;

		public UnionFind(int n, TValue v0, Func<TValue, TValue, TValue> composition, Func<TValue, TValue> inverse)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i, Value = v0 };
			SetsCount = n;
			this.composition = composition;
			this.inverse = inverse;
		}

		public int ItemsCount => nodes.Length;
		public int SetsCount { get; private set; }

		public TValue this[int x]
		{
			get
			{
				Find(x);
				return nodes[x].Value;
			}
		}

		public Node Find(int x) => Find(nodes[x]);
		Node Find(Node n)
		{
			if (n.Parent == null)
			{
				return n;
			}
			else
			{
				var root = Find(n.Parent);
				n.Value = composition(n.Value, n.Parent.Value);
				return n.Parent = root;
			}
		}

		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public bool Union(int x, int y, TValue x2y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			var v = composition(inverse(nodes[y].Value), composition(x2y, nodes[x].Value));
			if (nx.Size < ny.Size)
			{
				(nx, ny) = (ny, nx);
				v = inverse(v);
			}
			ny.Parent = nx;
			ny.Value = v;
			nx.Size += ny.Size;
			--SetsCount;
			return true;
		}

		public ILookup<Node, Node> ToGroups() => nodes.ToLookup(Find);
	}
}
