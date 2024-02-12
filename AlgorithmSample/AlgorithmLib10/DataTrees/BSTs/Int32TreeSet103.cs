using System;

namespace AlgorithmLab.DataTrees.BSTs.Trees103
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32TreeSet
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Enabled = {Enabled}")]
		public class Node
		{
			public int Key;
			public bool Enabled;
			public Node Left;
			public Node Right;
		}

		Node Root = new Node();
		public long Count;

		public void Clear()
		{
			Root = new Node();
			Count = 0;
		}

		public bool Contains(int item)
		{
			var node = GetNode(item);
			return node != null && node.Enabled;
		}

		public bool Remove(int item)
		{
			var node = GetNode(item);
			if (node == null || !node.Enabled) return false;

			node.Enabled = false;
			--Count;
			return true;
		}

		public bool Add(int item)
		{
			var node = GetOrAddNode(item);
			if (node.Enabled) return false;

			node.Enabled = true;
			++Count;
			return true;
		}

		#region Private Methods

		Node GetNode(int item)
		{
			var node = Root;
			while (node != null)
			{
				var d = item.CompareTo(node.Key);
				if (d == 0) break;
				if (d < 0) node = node.Left;
				else node = node.Right;
			}
			return node;
		}

		Node GetOrAddNode(int item)
		{
			Node node = Root, p = null;
			while (true)
			{
				var d = item.CompareTo(node.Key);
				if (d == 0) break;

				var lca = GetLca(item, node.Key);
				if (lca == node.Key)
				{
					p = node;
					if (d < 0)
					{
						if (node.Left == null)
						{
							node = node.Left = new Node { Key = item };
							break;
						}
						node = node.Left;
					}
					else
					{
						if (node.Right == null)
						{
							node = node.Right = new Node { Key = item };
							break;
						}
						node = node.Right;
					}
				}
				else if (lca == item)
				{
					var mn = new Node { Key = item };

					if (item < p.Key) p.Left = mn;
					else p.Right = mn;

					if (d < 0) mn.Right = node;
					else mn.Left = node;

					node = mn;
					break;
				}
				else
				{
					var mn = new Node { Key = lca };

					if (lca < p.Key) p.Left = mn;
					else p.Right = mn;

					if (d < 0)
					{
						mn.Right = node;
						node = mn.Left = new Node { Key = item };
					}
					else
					{
						mn.Left = node;
						node = mn.Right = new Node { Key = item };
					}
					break;
				}
			}
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

		#endregion
	}
}
