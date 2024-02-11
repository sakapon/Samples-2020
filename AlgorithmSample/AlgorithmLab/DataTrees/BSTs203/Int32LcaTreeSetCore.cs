using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.BSTs.BSTs203
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	class Int32LcaTreeSetCore<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Enabled = {Enabled}, Count = {Count}")]
		public class Node
		{
			public int Key;
			public TValue Value;
			public bool Enabled;
			public Node Left, Right;
			public long Count;
		}

		public Node Root = new Node();
		public long Count => Root.Count;
		internal readonly List<Node> Path = new List<Node>();

		public void Clear()
		{
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
			Node node = Root, p = null;
			while (true)
			{
				Path.Add(node);
				var d = key.CompareTo(node.Key);
				if (d == 0) return node;

				var lca = GetLca(key, node.Key);
				if (lca == node.Key)
				{
					p = node;
					ref var child = ref node.Right;
					if (d < 0) child = ref node.Left;

					if (child == null)
					{
						child = new Node { Key = key };
						Path.Add(child);
						return child;
					}
					node = child;
				}
				else if (lca == key)
				{
					var mn = new Node { Key = key };

					if (key < p.Key) p.Left = mn;
					else p.Right = mn;

					if (d < 0) mn.Right = node;
					else mn.Left = node;

					Path.Add(mn);
					return mn;
				}
				else
				{
					var mn = new Node { Key = lca };
					Path.Add(mn);

					if (lca < p.Key) p.Left = mn;
					else p.Right = mn;

					if (d < 0)
					{
						mn.Right = node;
						node = mn.Left = new Node { Key = key };
					}
					else
					{
						mn.Left = node;
						node = mn.Right = new Node { Key = key };
					}
					Path.Add(node);
					return node;
				}
			}
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
	}
}
