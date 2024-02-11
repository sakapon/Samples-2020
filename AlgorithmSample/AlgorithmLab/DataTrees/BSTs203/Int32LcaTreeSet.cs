using System;

namespace AlgorithmLab.DataTrees.BSTs.BSTs203
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeSet
	{
		readonly Int32LcaTreeSetCore<bool> core = new Int32LcaTreeSetCore<bool>();
		public long Count => core.Count;
		public void Clear() => core.Clear();

		public bool Contains(int item)
		{
			var node = core.GetNode(item);
			return node != null && node.Enabled;
		}

		public bool Remove(int item)
		{
			var node = core.GetNode(item);
			if (node == null || !node.Enabled) return false;

			node.Enabled = false;
			foreach (var n in core.Path) --n.Count;
			return true;
		}

		public bool Add(int item)
		{
			var node = core.GetOrAddNode(item);
			if (node.Enabled) return false;

			node.Enabled = true;
			foreach (var n in core.Path) ++n.Count;
			return true;
		}
	}
}
