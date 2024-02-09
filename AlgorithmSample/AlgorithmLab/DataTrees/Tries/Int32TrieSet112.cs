using System;

namespace AlgorithmLab.DataTrees.Tries.Tries112
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class Int32TrieSet
	{
		const int MaxDigit = 30;
		int[] l, r;
		bool[] u;
		int t;
		public long Count;

		public Int32TrieSet(int size = 1 << 22)
		{
			Initialize(size);
		}

		public void Clear()
		{
			Initialize(u.Length);
		}

		void Initialize(int size)
		{
			l = new int[size];
			r = new int[size];
			u = new bool[size];
			Array.Fill(l, -1);
			Array.Fill(r, -1);
			t = 0;
			Count = 0;
		}

		int GetNode(int item)
		{
			var node = 0;
			for (var f = 1 << MaxDigit; f != 0 && item != 0; f >>= 1)
			{
				if (item < 0)
				{
					if ((node = l[node]) == -1) break;
					item += f;
				}
				else
				{
					if ((node = r[node]) == -1) break;
					item -= f;
				}
			}
			return node;
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
			var node = 0;
			for (var f = 1 << MaxDigit; f != 0 && item != 0; f >>= 1)
			{
				if (item < 0)
				{
					if (l[node] == -1) l[node] = ++t;
					node = l[node];
					item += f;
				}
				else
				{
					if (r[node] == -1) r[node] = ++t;
					node = r[node];
					item -= f;
				}
			}
			if (u[node]) return false;

			u[node] = true;
			++Count;
			return true;
		}
	}
}
