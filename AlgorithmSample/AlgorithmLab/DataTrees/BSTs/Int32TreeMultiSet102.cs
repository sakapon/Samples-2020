using System;

namespace AlgorithmLab.DataTrees.BSTs.Trees102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32TrieMultiSet
	{
		[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
		public class Node
		{
			public long Count;
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

		public long GetCount(int item)
		{
			var node = GetNode(item);
			return node == null ? 0 : node.Count;
		}

		public void Add(int item, long count = 1)
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
			node.Count += count;
			Count += count;
		}
	}
}
