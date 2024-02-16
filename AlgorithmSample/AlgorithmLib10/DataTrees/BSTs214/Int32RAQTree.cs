
namespace AlgorithmLib10.DataTrees.BSTs.BSTs214
{
	public class Int32RAQTree
	{
		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		long[] values;
		int[] l, r;
		int t;

		readonly List<int> Path = new List<int>();

		public Int32RAQTree(int size = 1 << 22)
		{
			Initialize(size);
		}

		public void Clear()
		{
			Initialize(l.Length);
		}

		void Initialize(int size)
		{
			values = new long[size];
			l = new int[size];
			r = new int[size];
			Array.Fill(l, -1);
			Array.Fill(r, -1);
			t = 0;
		}

		public long this[int key]
		{
			get
			{
				ScanNode(key);
				var r = 0L;
				foreach (var n in Path) r += values[n];
				return r;
			}
			set => this[key, key + 1] = value;
		}

		public long this[int l, int r]
		{
			set
			{
				AddNode(l, r);
				foreach (var n in Path) values[n] += value;
			}
		}

		void AddNode(int l, int r)
		{
			Path.Clear();
			var root = 0;
			AddNode(ref root, -1 << MaxDigit, 1 << MaxDigit, l, r);
		}

		void AddNode(ref int node, int nl, int nr, int l, int r)
		{
			if (node == -1) node = ++t;
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = (nl + nr) >> 1;
			if (l < nc) AddNode(ref this.l[node], nl, nc, l, nc < r ? nc : r);
			if (nc < r) AddNode(ref this.r[node], nc, nr, l < nc ? nc : l, r);
		}

		void ScanNode(int key)
		{
			Path.Clear();
			var node = 0;
			var nc = 0;
			for (var d = 1 << MaxDigit - 1; ; d >>= 1)
			{
				if (node == -1) return;
				Path.Add(node);
				if (key < nc)
				{
					node = l[node];
					nc -= d;
				}
				else
				{
					node = r[node];
					nc |= d;
				}
			}
		}
	}
}
