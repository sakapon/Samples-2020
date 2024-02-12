namespace AlgorithmLib10.DataTrees.BSTs.BSTs203
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	class Int32LcaTreeSetCore<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Count = {Count}")]
		public class Node
		{
			public int Key;
			public TValue Value;
			public bool Enabled;
			public long Count;
			public Node Left, Right;
		}

		public Node Root = new Node();
		public long Count => Root.Count;
		internal readonly List<Node> Path = new List<Node>();

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

		public long GetIndex(int key)
		{
			var index = 0L;
			var node = Root;
			while (node != null)
			{
				var lc = node.Left?.Count ?? 0;
				var d = key.CompareTo(node.Key);
				if (d == 0)
				{
					if (!node.Enabled) break;
					return index + lc;
				}
				else if (d < 0)
				{
					node = node.Left;
				}
				else
				{
					index += lc;
					if (node.Enabled) ++index;
					node = node.Right;
				}
			}
			return -1;
		}

		public Node GetNodeAt(long index)
		{
			Path.Clear();
			var node = Root;
			while (node != null)
			{
				Path.Add(node);
				var lc = node.Left?.Count ?? 0;
				if (index < lc)
				{
					node = node.Left;
				}
				else
				{
					index -= lc;
					if (index == 0 && node.Enabled) break;
					if (node.Enabled) --index;
					node = node.Right;
				}
			}
			return node;
		}

		public Node GetOrAddNode(int key)
		{
			Path.Clear();
			Node node = Root, p = null;
			while (true)
			{
				var d = key.CompareTo(node.Key);
				if (d == 0)
				{
					Path.Add(node);
					return node;
				}

				var lca = GetLca(key, node.Key);
				if (lca == node.Key)
				{
					Path.Add(node);
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
					var mn = new Node { Key = key, Count = node.Count };

					if (key < p.Key) p.Left = mn;
					else p.Right = mn;

					if (d < 0) mn.Right = node;
					else mn.Left = node;

					Path.Add(mn);
					return mn;
				}
				else
				{
					var mn = new Node { Key = lca, Count = node.Count };

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

					Path.Add(mn);
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

		// Path に記録しません。
		public static Node GetNodeAt(Node node, long index)
		{
			if (node == null) return null;
			var lc = node.Left?.Count ?? 0;
			if (index < lc)
			{
				return GetNodeAt(node.Left, index);
			}
			else
			{
				index -= lc;
				if (index == 0 && node.Enabled) return node;
				if (node.Enabled) --index;
				return GetNodeAt(node.Right, index);
			}
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeSet : IEnumerable<int>
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

		public int GetFirst() => GetAt(0);
		public int GetLast() => GetAt(core.Count - 1);
		public int GetAt(long index)
		{
			var node = core.GetNodeAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
			return node.Key;
		}

		public int RemoveFirst() => RemoveAt(0);
		public int RemoveLast() => RemoveAt(core.Count - 1);
		public int RemoveAt(long index)
		{
			var node = core.GetNodeAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));

			node.Enabled = false;
			foreach (var n in core.Path) --n.Count;
			return node.Key;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public int[] ToArray()
		{
			var r = new int[core.Root.Count];
			var i = -1;
			Rec(core.Root);
			return r;

			void Rec(Int32LcaTreeSetCore<bool>.Node node)
			{
				if (node.Left != null) Rec(node.Left);
				if (node.Enabled) r[++i] = node.Key;
				if (node.Right != null) Rec(node.Right);
			}
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeMap<TValue> : IEnumerable<(int key, TValue value)>
	{
		readonly Int32LcaTreeSetCore<TValue> core = new Int32LcaTreeSetCore<TValue>();
		public long Count => core.Count;
		public void Clear() => core.Clear();

		readonly TValue iv;
		public Int32LcaTreeMap(TValue iv = default)
		{
			this.iv = iv;
		}

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
				if (node == null || !node.Enabled) return iv;
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

		public (int key, TValue value) GetFirst() => GetAt(0);
		public (int key, TValue value) GetLast() => GetAt(core.Count - 1);
		public (int key, TValue value) GetAt(long index)
		{
			var node = core.GetNodeAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
			return (node.Key, node.Value);
		}

		public (int key, TValue value) RemoveFirst() => RemoveAt(0);
		public (int key, TValue value) RemoveLast() => RemoveAt(core.Count - 1);
		public (int key, TValue value) RemoveAt(long index)
		{
			var node = core.GetNodeAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));

			node.Enabled = false;
			foreach (var n in core.Path) --n.Count;
			return (node.Key, node.Value);
		}

		public void SetAt(long index, TValue value)
		{
			var node = core.GetNodeAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
			node.Value = value;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<(int key, TValue value)> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public (int key, TValue value)[] ToArray()
		{
			var r = new (int, TValue)[core.Root.Count];
			var i = -1;
			Rec(core.Root);
			return r;

			void Rec(Int32LcaTreeSetCore<TValue>.Node node)
			{
				if (node.Left != null) Rec(node.Left);
				if (node.Enabled) r[++i] = (node.Key, node.Value);
				if (node.Right != null) Rec(node.Right);
			}
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LcaTreeMultiSet
	{
		readonly Int32LcaTreeSetCore<long> core = new Int32LcaTreeSetCore<long>();
		public long Count => core.Root.Value;
		public void Clear() => core.Clear();

		public bool Contains(int item)
		{
			var node = core.GetNode(item);
			return node != null && node.Enabled;
		}

		public bool Add(int item, long count = 1)
		{
			var node = core.GetOrAddNode(item);
			var nv = node.Value + count;
			if (nv < 0) return false;

			node.Value = nv;
			node.Enabled = nv != 0;
			foreach (var n in core.Path) n.Count += count;
			return true;
		}
	}
}
