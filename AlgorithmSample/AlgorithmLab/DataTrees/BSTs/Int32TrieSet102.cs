using System;

namespace AlgorithmLab.DataTrees.Tries.Tries102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32TrieSet
	{
		[System.Diagnostics.DebuggerDisplay(@"Enabled = {Enabled}")]
		public class Node
		{
			public bool Enabled;
			public Node Left;
			public Node Right;
		}

		const int MaxDigit = 30;
		Node Root = new Node();
		public long Count;

		public void Clear()
		{
			Root = new Node();
			Count = 0;
		}

		Node GetNode(int item)
		{
			var node = Root;
			for (var f = 1 << MaxDigit; f != 0 && item != 0; f >>= 1)
			{
				if (item < 0)
				{
					if ((node = node.Left) == null) break;
					item += f;
				}
				else
				{
					if ((node = node.Right) == null) break;
					item -= f;
				}
			}
			return node;
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
			var node = Root;
			for (var f = 1 << MaxDigit; f != 0 && item != 0; f >>= 1)
			{
				if (item < 0)
				{
					node = node.Left ??= new Node();
					item += f;
				}
				else
				{
					node = node.Right ??= new Node();
					item -= f;
				}
			}
			if (node.Enabled) return false;

			node.Enabled = true;
			++Count;
			return true;
		}
	}
}
