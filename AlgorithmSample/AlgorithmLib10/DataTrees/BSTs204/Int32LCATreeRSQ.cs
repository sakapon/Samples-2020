
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public class Int32LCATreeRSQ
	{
		// ノードは区間を表します。
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Value = {Value}")]
		public class Node
		{
			// 区間の中央
			public int Key;
			// 区間の長さの半分 (葉の場合は 0)
			public int HalfLength;
			public long Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public Int32LCATreeRSQ() => Clear();
		public void Clear() => Root = new Node { HalfLength = 1 << MaxDigit };

		public long this[int key] => Get(key, key + 1);
		public long this[int l, int r] => Get(l, r);

		public void Add(int key, long value)
		{
			GetOrAddNode(key);
			foreach (var n in Path) n.Value += value;
		}

		long Get(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		void ScanNode(Node node, int l, int r)
		{
			if (node == null) return;
			var nc = node.Key;
			if (l <= nc - node.HalfLength && nc + node.HalfLength <= r) { Path.Add(node); return; }
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

				var d = key.CompareTo(node.Key);
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
			var nc = node.Key;
			if (nc - node.HalfLength <= key && key < nc + node.HalfLength) return null;
			var f = MaxBit(nc ^ key);
			var lca = new Node { Key = key & ~(f - 1) | f, HalfLength = f, Value = node.Value };
			(nc - node.HalfLength < lca.Key ? ref lca.Left : ref lca.Right) = node;
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
