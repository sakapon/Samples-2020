
namespace AlgorithmLib10.DataTrees.BSTs.BSTs203
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

	class Int32LCATreeMergeCore<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Key = {Key}, Value = {Value}")]
		public class Node
		{
			public int Key;
			public TValue Value, MergedValue;
			public Node Left, Right;
		}

		readonly Monoid<TValue> monoid;
		public Node Root = new Node();
		public readonly List<Node> Path = new List<Node>();

		public Int32LCATreeMergeCore(Monoid<TValue> monoid)
		{
			this.monoid = monoid;
		}

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
				if (node == null) { node = new Node { Key = key, Value = monoid.Id }; break; }

				var d = key.CompareTo(node.Key);
				if (d == 0) break;

				var lca = GetLca(key, node.Key);
				if (lca != node.Key)
				{
					var child = node;
					node = new Node { Key = lca, Value = monoid.Id };
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

		// [l, r)
		public TValue Merge(int l, int r) => Merge(Root, l, r);

		public TValue Merge(Node node, int l, int r)
		{
			if (node == null) return monoid.Id;
			if (l >= r) return monoid.Id;
			var dl = l.CompareTo(node.Key);
			var dr = r.CompareTo(node.Key);
			if (dr < 0) return Merge(node.Left, l, r);
			if (dr == 0) return MergeRight(node.Left, l);
			if (dl > 0) return Merge(node.Right, l, r);
			var mv = monoid.Op(node.Value, MergeLeft(node.Right, r));
			if (dl == 0) return mv;
			return monoid.Op(mv, MergeRight(node.Left, l));
		}

		public TValue MergeRight(Node node, int l)
		{
			if (node == null) return monoid.Id;
			var d = l.CompareTo(node.Key);
			if (d > 0) return MergeRight(node.Right, l);
			var mv = monoid.Op(node.Value, node.Right != null ? node.Right.MergedValue : monoid.Id);
			if (d == 0) return mv;
			return monoid.Op(mv, MergeRight(node.Left, l));
		}

		public TValue MergeLeft(Node node, int r)
		{
			if (node == null) return monoid.Id;
			var d = r.CompareTo(node.Key);
			if (d < 0) return MergeLeft(node.Left, r);
			var mv = node.Left != null ? node.Left.MergedValue : monoid.Id;
			if (d == 0) return mv;
			mv = monoid.Op(mv, node.Value);
			return monoid.Op(mv, MergeLeft(node.Right, r));
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32LCATreeMerge<TValue>
	{
		readonly Monoid<TValue> monoid;
		readonly Int32LCATreeMergeCore<TValue> core;
		public void Clear() => core.Clear();

		public Int32LCATreeMerge(Monoid<TValue> monoid)
		{
			this.monoid = monoid;
			core = new Int32LCATreeMergeCore<TValue>(monoid);
		}

		public TValue this[int key]
		{
			get
			{
				var node = core.GetNode(key);
				return node != null ? node.Value : monoid.Id;
			}
			set
			{
				var node = core.GetOrAddNode(key);
				node.Value = value;

				for (int i = core.Path.Count - 1; i >= 0; i--)
				{
					var n = core.Path[i];
					n.MergedValue = monoid.Op(n.Value, n.Left != null ? n.Left.MergedValue : monoid.Id);
					n.MergedValue = monoid.Op(n.MergedValue, n.Right != null ? n.Right.MergedValue : monoid.Id);
				}
			}
		}

		public TValue this[int l, int r] => core.Merge(l, r);
	}
}
