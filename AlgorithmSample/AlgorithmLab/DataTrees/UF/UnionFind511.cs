using System;
using System.Linq;

// 実用可能です。
// Int32 vertexes, node-based, data augmentation
namespace AlgorithmLab.DataTrees.UF511
{
	[System.Diagnostics.DebuggerDisplay(@"ItemsCount = {ItemsCount}, SetsCount = {SetsCount}")]
	public class UnionFind<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay}\}")]
		public class Node
		{
			public int Item;
			public Node Parent;
			public int Size = 1;
			public TValue Value;
			string DebuggerDisplay => Parent == null ? $"{{{Item}, Size = {Size}, Value = {Value}}}" : $"{{{Item} (Not Root)}}";
		}

		readonly Node[] nodes;
		public int ItemsCount => nodes.Length;
		public int SetsCount { get; private set; }
		public Func<TValue, TValue, TValue> MergeValues { get; }

		public UnionFind(int n, Func<TValue, TValue, TValue> mergeValues, TValue[] values = null)
		{
			MergeValues = mergeValues;
			values ??= new TValue[n];
			nodes = new Node[n];
			for (int i = 0; i < n; ++i) nodes[i] = new Node { Item = i, Value = values[i] };
			SetsCount = n;
		}

		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);
		public Node Find(int x) => Find(nodes[x]);
		public bool AreSame(int x, int y) => Find(x) == Find(y);
		public int GetSize(int x) => Find(x).Size;
		public TValue GetValue(int x) => Find(x).Value;

		public bool Union(int x, int y)
		{
			var rx = Find(x);
			var ry = Find(y);
			if (rx == ry) return false;

			// 左右の順序を保って値をマージします。
			var v = MergeValues(rx.Value, ry.Value);

			if (rx.Size < ry.Size) (rx, ry) = (ry, rx);
			ry.Parent = rx;
			rx.Size += ry.Size;
			--SetsCount;
			rx.Value = v;
			return true;
		}

		public ILookup<Node, Node> ToSets() => nodes.ToLookup(Find);
	}
}
