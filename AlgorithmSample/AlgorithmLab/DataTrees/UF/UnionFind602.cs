using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF602
{
	// Int32 vertexes
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind<TOp>
	{
		// Op: 親を基準とした相対値
		[System.Diagnostics.DebuggerDisplay(@"\{{Key}, {Op}\}")]
		public class Node
		{
			public int Key;
			public TOp Op;
			public Node Parent;
			public int Size = 1;
		}

		readonly Node[] nodes;
		readonly Func<TOp, TOp> inverse;
		readonly Func<TOp, TOp, TOp> composition;

		public UnionFind(int n, TOp op0, Func<TOp, TOp> inverse, Func<TOp, TOp, TOp> composition)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i, Op = op0 };
			SetsCount = n;
			this.inverse = inverse;
			this.composition = composition;
		}

		public int ItemsCount => nodes.Length;
		public int SetsCount { get; private set; }

		public TOp this[int x]
		{
			get
			{
				Find(x);
				return nodes[x].Op;
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
				n.Op = composition(n.Op, n.Parent.Op);
				return n.Parent = root;
			}
		}

		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public bool Union(int x, int y, TOp x2y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			var v = composition(inverse(nodes[y].Op), composition(x2y, nodes[x].Op));
			if (nx.Size < ny.Size)
			{
				(nx, ny) = (ny, nx);
				v = inverse(v);
			}
			ny.Parent = nx;
			ny.Op = v;
			nx.Size += ny.Size;
			--SetsCount;
			return true;
		}

		public bool Verify(int x, int y, TOp x2y) => AreSame(x, y) && EqualityComparer<TOp>.Default.Equals(nodes[y].Op, composition(x2y, nodes[x].Op));

		public ILookup<Node, Node> ToGroups() => nodes.ToLookup(Find);
	}
}
