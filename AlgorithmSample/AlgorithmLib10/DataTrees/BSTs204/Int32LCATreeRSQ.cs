
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public class Int32LCATreeRSQ
	{
		// 区間を表します。
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Value = {Value}")]
		public class Node
		{
			// 区間の左端
			public int Key;
			// 区間の長さの半分
			public int HalfLength;
			public long Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		Node Root = new Node { Key = -1 << MaxDigit, HalfLength = 1 << MaxDigit };
		readonly List<Node> Path = new List<Node>();

		public void Clear() => Root = new Node { Key = -1 << MaxDigit, HalfLength = MaxDigit };

		public long this[int key] => this[key, key + 1];
		public long this[int l, int r]
		{
			get
			{
				ScanNode(l, r);
				var v = 0L;
				foreach (var n in Path) v += n.Value;
				return v;
			}
		}

		public void Add(int key, long value)
		{
			var node = GetOrAddNode(key);
			foreach (var n in Path) n.Value += value;
		}

		void ScanNode(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
		}

		void ScanNode(Node node, int l, int r)
		{
			if (node == null) return;
			if (l <= node.Key && node.Key + (node.HalfLength << 1) <= r) { Path.Add(node); return; }
			var nc = node.Key + node.HalfLength;
			if (l < nc) ScanNode(node.Left, l, nc < r ? nc : r);
			if (nc < r) ScanNode(node.Right, l < nc ? nc : l, r);
		}

		Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == null) { node = new Node { Key = key }; break; }

				var d = key.CompareTo(node.Key + node.HalfLength);
				if (d == 0 && node.HalfLength == 0) break;

				var lca = GetLca(node, key);
				if (lca == null)
				{
					Path.Add(node);
					node = ref (d < 0 ? ref node.Left : ref node.Right);
				}
				else
				{
					node = lca;
					Path.Add(node);
					node = ref (node.Left == null ? ref node.Left : ref node.Right);
				}
			}

			Path.Add(node);
			return node;
		}

		static Node GetLca(Node node, int key)
		{
			var l = node.Key;
			if (l <= key && key < l + (node.HalfLength << 1)) return null;
			var f = MaxBit(l ^ key);
			var lca = new Node { Key = l & ~(f | (f - 1)), HalfLength = f, Value = node.Value };
			(l < lca.Key + lca.HalfLength ? ref lca.Left : ref lca.Right) = node;
			return lca;
		}

		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}
	}
}
