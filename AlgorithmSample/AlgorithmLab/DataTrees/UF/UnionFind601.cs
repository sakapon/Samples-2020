using System;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF601
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
		readonly Func<TValue, TValue, TValue> addValues;
		readonly Func<int, int, TValue> diff;

		public UnionFind(int n, Func<TValue, TValue, TValue> addValues, Func<int, int, TValue> diff)
		{
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Key = i };
			SetsCount = n;
			this.addValues = addValues;
			this.diff = diff;
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
				n.Value = addValues(n.Parent.Value, n.Value);
				return n.Parent = root;
			}
		}

		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public bool Union(int x, int y)
		{
			var nx = Find(x);
			var ny = Find(y);
			if (nx == ny) return false;

			if (nx.Size < ny.Size) (nx, ny) = (ny, nx);
			ny.Parent = nx;
			ny.Value = diff(nx.Key, ny.Key);
			nx.Size += ny.Size;
			--SetsCount;
			return true;
		}

		public ILookup<Node, Node> ToGroups() => nodes.ToLookup(Find);
	}
}
