using System;
using System.Collections.Generic;
using AlgorithmLab.DataTrees.PQ.HeapQueue201;

// priority queue を利用します。
namespace AlgorithmLab.Graphs.SPPs.Dijkstra251
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<(int to, long cost)> Edges { get; } = new List<(int, long)>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex Previous { get; set; }
		public Vertex(int id) { Id = id; }

		public int[] GetPathVertexes()
		{
			var path = new Stack<int>();
			for (var v = this; v != null; v = v.Previous)
				path.Push(v.Id);
			return path.ToArray();
		}

		public (int, int)[] GetPathEdges()
		{
			var path = new Stack<(int, int)>();
			for (var v = this; v.Previous != null; v = v.Previous)
				path.Push((v.Previous.Id, v.Id));
			return path.ToArray();
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class Dijkstra
	{
		public Vertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public Vertex this[int v] => Vertexes[v];

		public Dijkstra(int n)
		{
			Vertexes = new Vertex[n];
			for (int v = 0; v < n; ++v) Vertexes[v] = new Vertex(v);
		}
		public Dijkstra(int n, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public Dijkstra(int n, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public void AddEdge(int from, int to, bool twoWay, long cost)
		{
			Vertexes[from].Edges.Add((to, cost));
			if (twoWay) Vertexes[to].Edges.Add((from, cost));
		}

		public void Execute(int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var q = new HeapQueue<long, int>(false);
			q.Add(0, sv);

			while (q.Count > 0)
			{
				var (c, v) = q.Pop();
				if (v == ev) return;
				var vo = Vertexes[v];
				if (vo.Cost < c) continue;

				foreach (var (nv, cost) in vo.Edges)
				{
					var nvo = Vertexes[nv];
					var nc = c + cost;
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Previous = vo;
					q.Add(nc, nv);
				}
			}
		}
	}
}
