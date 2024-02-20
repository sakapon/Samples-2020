
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public class Int32LCARAQTree
	{
		// ノードは区間を表します。
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Value = {Value}")]
		public class Node
		{
			public int L, R;
			public long Value;
			public Node Left, Right;
		}

		// [0, 1 << MaxDigit)
		const int MaxDigit = 30;
		protected Node Root;
		protected readonly List<Node> Path = new List<Node>();

		public Int32LCARAQTree() => Clear();
		public void Clear() => Root = new Node { R = 1 << MaxDigit };

		protected void ScanOrAddNodes(int l, int r)
		{
			Path.Clear();
			ScanOrAddNodes(ref Root, l, r);
		}

		void ScanOrAddNodes(ref Node node, int l, int r)
		{
			if (node == null)
			{
				if (l + 1 == r)
				{
					node = new Node { L = l, R = r };
					Path.Add(node); return;
				}
				else
				{
					var f = MaxBit(l ^ (r - 1));
					var nl = l & ~(f | (f - 1));
					node = new Node { L = nl, R = nl + (f << 1) };
				}
			}

			if (node.L == l && r == node.R) { Path.Add(node); return; }

			if (!(node.L <= l && r <= node.R))
			{
				var child = node;
				var nl = node.L < l ? node.L : l;
				var f = MaxBit(nl ^ (node.R > r ? node.R - 1 : r - 1));
				nl &= ~(f | (f - 1));
				node = new Node { L = nl, R = nl + (f << 1) };
				(child.L < (nl | f) ? ref node.Left : ref node.Right) = child;
			}

			var nc = (node.L + node.R) >> 1;
			if (l < nc) ScanOrAddNodes(ref node.Left, l, nc < r ? nc : r);
			if (nc < r) ScanOrAddNodes(ref node.Right, l < nc ? nc : l, r);
		}

		protected Node GetNode(int key)
		{
			Path.Clear();
			var node = Root;
			while (node != null)
			{
				if (!(node.L <= key && key < node.R)) return null;

				Path.Add(node);
				var nc = (node.L + node.R) >> 1;
				var d = key.CompareTo(nc);
				if (d == 0 && node.L + 1 == node.R) break;
				node = d < 0 ? node.Left : node.Right;
			}
			return node;
		}

		protected Node GetOrAddNode(int key)
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
					node = new Node { L = l, R = l + (f << 1) };
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

		public long this[int key] => Get(key);

		public long Get(int key)
		{
			GetNode(key);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		public void Add(int key, long value)
		{
			var node = GetOrAddNode(key);
			node.Value += value;
		}

		public void Add(int l, int r, long value)
		{
			ScanOrAddNodes(l, r);
			foreach (var n in Path) n.Value += value;
		}
	}
}
