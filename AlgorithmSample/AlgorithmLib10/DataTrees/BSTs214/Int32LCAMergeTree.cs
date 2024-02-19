﻿
namespace AlgorithmLib10.DataTrees.BSTs.BSTs214
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
		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;

		// ノードは区間を表します。
		int[] L, R;
		protected TValue[] Value;
		protected int[] Left, Right;
		// 最後に発行されたノード ID
		int t;

		protected int Root;
		protected readonly List<int> Path = new List<int>();
		protected abstract TValue IV { get; }

		public Int32LCAMergeTreeCore(int size = 1 << 20) => Initialize(size);
		public void Clear() => Initialize(Value.Length);
		void Initialize(int size)
		{
			L = new int[size];
			R = new int[size];
			Value = new TValue[size];
			Left = new int[size];
			Right = new int[size];

			L[Root] = -1 << MaxDigit;
			R[Root] = 1 << MaxDigit;
			Array.Fill(Value, IV);
			Array.Fill(Left, -1);
			Array.Fill(Right, -1);
			t = 0;
		}

		protected void ScanNode(int node, int l, int r)
		{
			if (node == -1) return;
			var nc = (L[node] + R[node]) >> 1;
			if (l <= L[node] && R[node] <= r) { Path.Add(node); return; }
			if (l < nc) ScanNode(Left[node], l, nc < r ? nc : r);
			if (nc < r) ScanNode(Right[node], l < nc ? nc : l, r);
		}

		protected int GetNode(int key)
		{
			Path.Clear();
			var node = Root;
			while (node != -1)
			{
				if (!(L[node] <= key && key < R[node])) return -1;

				Path.Add(node);
				var nc = (L[node] + R[node]) >> 1;
				var d = key.CompareTo(nc);
				if (d == 0 && L[node] + 1 == R[node]) break;
				node = d < 0 ? Left[node] : Right[node];
			}
			return node;
		}

		protected int GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == -1)
				{
					node = ++t;
					L[node] = key;
					R[node] = key + 1;
					break;
				}

				var nc = (L[node] + R[node]) >> 1;
				if (L[node] <= key && key < R[node])
				{
					var d = key.CompareTo(nc);
					if (d == 0 && L[node] + 1 == R[node]) break;
					Path.Add(node);
					node = ref (d < 0 ? ref Left[node] : ref Right[node]);
				}
				else
				{
					var child = node;
					var f = MaxBit(nc ^ key);
					var l = key & ~(f | (f - 1));
					node = ++t;
					L[node] = l;
					R[node] = l + (f << 1);
					Value[node] = Value[child];
					Path.Add(node);
					if (nc < (l | f))
					{
						Left[node] = child;
						node = ref Right[node];
					}
					else
					{
						Right[node] = child;
						node = ref Left[node];
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
			get => Get(key);
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int key)
		{
			var node = GetNode(key);
			return node != -1 ? Value[node] : IV;
		}

		public TValue Get(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
			var v = IV;
			foreach (var n in Path) v = merge(v, Value[n]);
			return v;
		}

		public void Set(int key, TValue value)
		{
			var node = GetOrAddNode(key);
			Value[node] = value;
			for (int i = Path.Count - 2; i >= 0; --i)
			{
				node = Path[i];
				Value[node] = Left[node] != -1 ? Value[Left[node]] : IV;
				if (Right[node] != -1) Value[node] = merge(Value[node], Value[Right[node]]);
			}
		}
	}

	public class Int32LCARSQTree : Int32LCAMergeTreeCore<long>
	{
		protected override long IV => 0;

		public long this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public long this[int l, int r] => Get(l, r);

		public long Get(int key)
		{
			var node = GetNode(key);
			return node != -1 ? Value[node] : 0;
		}

		public long Get(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, l, r);
			var v = 0L;
			foreach (var n in Path) v += Value[n];
			return v;
		}

		public void Add(int key, long value)
		{
			GetOrAddNode(key);
			foreach (var n in Path) Value[n] += value;
		}

		public void Set(int key, long value)
		{
			var node = GetOrAddNode(key);
			var d = value - Value[node];
			foreach (var n in Path) Value[n] += d;
		}
	}
}
