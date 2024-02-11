using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.BSTs.BSTs203
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeMap<TValue>
	{
		readonly Int32LcaTreeSetCore<TValue> core = new Int32LcaTreeSetCore<TValue>();
		public long Count => core.Count;
		public void Clear() => core.Clear();

		public bool ContainsKey(int key)
		{
			var node = core.GetNode(key);
			return node != null && node.Enabled;
		}

		public bool Remove(int key)
		{
			var node = core.GetNode(key);
			if (node == null || !node.Enabled) return false;

			node.Enabled = false;
			foreach (var n in core.Path) --n.Count;
			return true;
		}

		public bool Add(int key, TValue value)
		{
			var node = core.GetOrAddNode(key);
			if (node.Enabled) return false;

			node.Enabled = true;
			node.Value = value;
			foreach (var n in core.Path) ++n.Count;
			return true;
		}

		public TValue this[int key]
		{
			get
			{
				var node = core.GetNode(key);
				if (node == null || !node.Enabled) throw new KeyNotFoundException();
				return node.Value;
			}
			set
			{
				var node = core.GetOrAddNode(key);
				if (node.Enabled)
				{
					node.Value = value;
					return;
				}
				node.Enabled = true;
				node.Value = value;
				foreach (var n in core.Path) ++n.Count;
			}
		}
	}
}
