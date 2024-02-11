using System;

namespace AlgorithmLab.DataTrees.Tries.Tries113
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32TrieSet
	{
		int[] keys;
		int[] l, r;
		bool[] u;
		int t;
		public long Count;

		public Int32TrieSet(int size = 1 << 20)
		{
			Initialize(size);
		}

		public void Clear()
		{
			Initialize(u.Length);
		}

		void Initialize(int size)
		{
			keys = new int[size];
			l = new int[size];
			r = new int[size];
			u = new bool[size];
			Array.Fill(l, -1);
			Array.Fill(r, -1);
			t = 0;
			Count = 0;
		}

		public bool Contains(int item)
		{
			var node = GetNode(item);
			return node != -1 && u[node];
		}

		public bool Remove(int item)
		{
			var node = GetNode(item);
			if (node == -1 || !u[node]) return false;

			u[node] = false;
			--Count;
			return true;
		}

		public bool Add(int item)
		{
			var node = GetOrAddNode(item);
			if (u[node]) return false;

			u[node] = true;
			++Count;
			return true;
		}

		#region Private Methods

		int GetNode(int item)
		{
			var node = 0;
			while (node != -1)
			{
				var d = item.CompareTo(keys[node]);
				if (d == 0) break;
				node = d < 0 ? l[node] : r[node];
			}
			return node;
		}

		int GetOrAddNode(int item)
		{
			int node = 0, p = -1;
			while (true)
			{
				var d = item.CompareTo(keys[node]);
				if (d == 0) break;

				var lca = GetLca(item, keys[node]);
				if (lca == keys[node])
				{
					p = node;
					if (d < 0)
					{
						if (l[node] == -1)
						{
							var nn = ++t;
							keys[nn] = item;
							node = l[node] = nn;
							break;
						}
						node = l[node];
					}
					else
					{
						if (r[node] == -1)
						{
							var nn = ++t;
							keys[nn] = item;
							node = r[node] = nn;
							break;
						}
						node = r[node];
					}
				}
				else if (lca == item)
				{
					var mn = ++t;
					keys[mn] = item;

					if (item < keys[p]) l[p] = mn;
					else r[p] = mn;

					if (d < 0) r[mn] = node;
					else l[mn] = node;

					node = mn;
					break;
				}
				else
				{
					var mn = ++t;
					keys[mn] = lca;

					if (lca < keys[p]) l[p] = mn;
					else r[p] = mn;

					var nn = ++t;
					keys[nn] = item;

					if (d < 0)
					{
						r[mn] = node;
						node = l[mn] = nn;
					}
					else
					{
						l[mn] = node;
						node = r[mn] = nn;
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
