
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public class Int32RAQTree
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public long Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public void Clear() => Root = null;

		public long this[int key]
		{
			get
			{
				ScanNode(key);
				var r = 0L;
				foreach (var n in Path) r += n.Value;
				return r;
			}
			set => this[key, key + 1] = value;
		}

		public long this[int l, int r]
		{
			set
			{
				AddNode(l, r);
				foreach (var n in Path) n.Value += value;
			}
		}

		void AddNode(int l, int r)
		{
			Path.Clear();
			AddNode(ref Root, -1 << MaxDigit, 1 << MaxDigit, l, r);
		}

		void AddNode(ref Node node, int nl, int nr, int l, int r)
		{
			node ??= new Node();
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = (nl + nr) >> 1;
			if (l < nc) AddNode(ref node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) AddNode(ref node.Right, nc, nr, l < nc ? nc : l, r);
		}

		void ScanNode(int key)
		{
			Path.Clear();
			var node = Root;
			var nc = 0;
			for (var d = 1 << MaxDigit - 1; ; d >>= 1)
			{
				if (node == null) return;
				Path.Add(node);
				if (key < nc)
				{
					node = node.Left;
					nc -= d;
				}
				else
				{
					node = node.Right;
					nc |= d;
				}
			}
		}
	}
}
