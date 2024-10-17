using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees.SUF401
{
	public class StaticUnionGraph
	{
		readonly int n;
		readonly List<int>[] map;

		public StaticUnionGraph(int n, List<int>[] map)
		{
			this.n = n;
			this.map = map;
		}
		public StaticUnionGraph(int n) : this(n, Array.ConvertAll(new bool[n], _ => new List<int>())) { }

		public StaticUnionGraph(int n, IEnumerable<(int u, int v)> es) : this(n)
		{
			foreach (var (u, v) in es) Union(u, v);
		}

		public void Union(int u, int v)
		{
			map[u].Add(v);
			map[v].Add(u);
		}

		public StaticFind Build()
		{
			var sets = new List<int[]>();
			var u = new bool[n];
			var q = new Queue<int>();
			var set = new List<int>();

			for (int sv = 0; sv < n; sv++)
			{
				if (u[sv]) continue;
				u[sv] = true;
				q.Enqueue(sv);
				set.Add(sv);

				while (q.TryDequeue(out var v))
				{
					foreach (var nv in map[v])
					{
						if (u[nv]) continue;
						u[nv] = true;
						q.Enqueue(nv);
						set.Add(nv);
					}
				}
				sets.Add(set.ToArray());
				set.Clear();
			}

			return new StaticFind(n, sets.ToArray());
		}
	}

	public class StaticFind
	{
		readonly int[][] map;
		public int[][] Sets { get; }

		public int ItemsCount => map.Length;
		public int SetsCount => Sets.Length;

		internal StaticFind(int n, int[][] sets)
		{
			Sets = sets;
			map = new int[n][];
			foreach (var set in sets)
				foreach (var v in set)
					map[v] = set;
		}

		public int[] Find(int x) => map[x];
		public bool AreSame(int x, int y) => map[x] == map[y];
		public int GetSize(int x) => map[x].Length;
	}
}
