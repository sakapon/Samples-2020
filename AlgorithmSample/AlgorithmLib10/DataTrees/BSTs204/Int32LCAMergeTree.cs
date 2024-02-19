
namespace AlgorithmLib10.DataTrees.BSTs.BSTs204
{
	public static class Monoid
	{
		public static Monoid<int> Int32_Add { get; } = new Monoid<int>((x, y) => x + y);
		public static Monoid<long> Int64_Add { get; } = new Monoid<long>((x, y) => x + y);
		public static Monoid<int> Int32_Min { get; } = new Monoid<int>((x, y) => x <= y ? x : y, int.MaxValue);
		public static Monoid<int> Int32_Max { get; } = new Monoid<int>((x, y) => x >= y ? x : y, int.MinValue);

		public static Monoid<int> Int32_ArgMin(int[] a) => new Monoid<int>((x, y) => a[x] <= a[y] ? x : y);

		public static Monoid<int> Int32_Update { get; } = new Monoid<int>((x, y) => x != int.MinValue ? x : y, int.MinValue);
	}

	public struct Monoid<T>
	{
		public Func<T, T, T> Op;
		public T Id;
		public Monoid(Func<T, T, T> op, T id = default(T)) { Op = op; Id = id; }
	}

	public abstract class Int32LCAMergeTreeCore<TValue>
	{
		// ノードは区間を表します。
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Value = {Value}")]
		public class Node
		{
			public int L, R;
			public TValue Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		protected Node Root;
		protected readonly List<Node> Path = new List<Node>();
		protected abstract TValue IV { get; }

		public Int32LCAMergeTreeCore() => Clear();
		public void Clear() => Root = new Node { L = -1 << MaxDigit, R = 1 << MaxDigit, Value = IV };

		protected void ScanNode(Node node, int l, int r)
		{
			if (node == null) return;
			var nc = (node.L + node.R) >> 1;
			if (l <= node.L && node.R <= r) { Path.Add(node); return; }
			if (l < nc) ScanNode(node.Left, l, nc < r ? nc : r);
			if (nc < r) ScanNode(node.Right, l < nc ? nc : l, r);
		}

		protected Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == null) { node = new Node { L = key, R = key + 1, Value = IV }; break; }

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
					node = new Node { L = l, R = l + (f << 1), Value = node.Value };
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
	}

	public class Int32LCAMergeTree<TValue> : Int32LCAMergeTreeCore<TValue>
	{
		readonly Func<TValue, TValue, TValue> merge;
		protected override TValue IV { get; }
		public Int32LCAMergeTree(Monoid<TValue> monoid) => (merge, IV) = (monoid.Op, monoid.Id);

		public TValue this[int key]
		{
			get => Get(key, key + 1);
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
			var v = IV;
			foreach (var n in Path) v = merge(v, n.Value);
			return v;
		}

		public void Set(int key, TValue value)
		{
			var node = GetOrAddNode(key);
			node.Value = value;
			for (int i = Path.Count - 2; i >= 0; --i)
			{
				node = Path[i];
				node.Value = node.Left != null ? node.Left.Value : IV;
				if (node.Right != null) node.Value = merge(node.Value, node.Right.Value);
			}
		}
	}

	public class Int32LCARSQTree : Int32LCAMergeTreeCore<long>
	{
		protected override long IV => 0;

		public long this[int key]
		{
			get => Get(key, key + 1);
			set => Set(key, value);
		}
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		public void Add(int key, long value)
		{
			GetOrAddNode(key);
			foreach (var n in Path) n.Value += value;
		}

		public void Set(int key, long value)
		{
			var node = GetOrAddNode(key);
			var d = value - node.Value;
			foreach (var n in Path) n.Value += d;
		}
	}
}
