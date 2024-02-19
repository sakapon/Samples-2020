
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public class Int32LCATreeRSQ
	{
		// ノードは区間を表します。
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Value = {Value}")]
		public class Node
		{
			public int L, R;
			public long Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public Int32LCATreeRSQ() => Clear();
		public void Clear() => Root = new Node { L = -1 << MaxDigit, R = 1 << MaxDigit };

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
			var nc = (node.L + node.R) >> 1;
			if (l <= node.L && node.R <= r) { Path.Add(node); return; }
			if (l < nc) ScanNode(node.Left, l, nc < r ? nc : r);
			if (nc < r) ScanNode(node.Right, l < nc ? nc : l, r);
		}

		Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == null) { node = new Node { L = key, R = key + 1 }; break; }

				var nc = (node.L + node.R) >> 1;
				if (node.L <= key && key < node.R)
				{
					var d = key.CompareTo(nc);
					if (d == 0 && node.L + 1 == node.R) break;
					Path.Add(node);
					node = ref (d < 0 ? ref node.Left : ref node.Right);
				}
				else
				{
					var child = node;
					var f = MaxBit(nc ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Value = node.Value };
					Path.Add(node);
					if (nc < (l | f))
					{
						node.Left = child;
						node = ref node.Right;
					}
					else
					{
						node.Right = child;
						node = ref node.Left;
					}
				}
			}

			Path.Add(node);
			return node;
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
