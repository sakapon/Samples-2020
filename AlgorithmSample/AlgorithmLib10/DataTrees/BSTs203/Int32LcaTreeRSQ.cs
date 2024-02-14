
namespace AlgorithmLib10.DataTrees.BSTs.BSTs203
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	class Int32LcaTreeRSQCore
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Count = {Count}")]
		public class Node
		{
			public int Key;
			public long Value;
			public long Count;
			public Node Left, Right;
		}

		public Node Root = new Node();
		public long Count => Root.Count;
		public readonly List<Node> Path = new List<Node>();

		public void Clear()
		{
			// 暗黙的に Key = 0
			Root = new Node();
		}

		public Node GetNode(int key)
		{
			Path.Clear();
			var node = Root;
			while (node != null)
			{
				Path.Add(node);
				var d = key.CompareTo(node.Key);
				if (d == 0) break;
				node = d < 0 ? node.Left : node.Right;
			}
			return node;
		}

		public Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == null) { node = new Node { Key = key }; break; }

				var d = key.CompareTo(node.Key);
				if (d == 0) break;

				var lca = GetLca(key, node.Key);
				if (lca != node.Key)
				{
					var child = node;
					node = new Node { Key = lca, Count = node.Count };
					(d < 0 ? ref node.Right : ref node.Left) = child;
					if (lca == key) break;
				}

				Path.Add(node);
				node = ref (d < 0 ? ref node.Left : ref node.Right);
			}

			Path.Add(node);
			return node;
		}

		static int GetLca(int x, int y)
		{
			if (x == 0 || y == 0) return 0;
			if ((-x & x) < (-y & y)) (x, y) = (y, x);
			if (x - (-x & x) < y && y < x + (-x & x)) return x;
			var f = MaxBit(x ^ y);
			return x & ~(f - 1) | f;
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

		public long GetCount(int l, int r) => GetCountLt(Root, r) - GetCountLt(Root, l);

		// (MinValue, key)
		public static long GetCountLt(Node node, int key)
		{
			if (node == null) return 0;
			var d = key.CompareTo(node.Key);
			if (d < 0) return GetCountLt(node.Left, key);
			var lc = node.Left?.Count ?? 0;
			if (d == 0) return lc;
			return lc + node.Value + GetCountLt(node.Right, key);
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeRSQ
	{
		readonly Int32LcaTreeRSQCore core = new Int32LcaTreeRSQCore();
		public long Count => core.Root.Value;
		public void Clear() => core.Clear();

		public void Add(int key, long value)
		{
			var node = core.GetOrAddNode(key);
			node.Value += value;
			foreach (var n in core.Path) n.Count += value;
		}

		public long this[int key]
		{
			get
			{
				var node = core.GetNode(key);
				return node != null ? node.Value : 0;
			}
			set
			{
				var node = core.GetOrAddNode(key);
				var d = value - node.Value;
				node.Value = value;
				foreach (var n in core.Path) n.Count += d;
			}
		}

		public long this[int l, int r] => core.GetCount(l, r);
	}
}
