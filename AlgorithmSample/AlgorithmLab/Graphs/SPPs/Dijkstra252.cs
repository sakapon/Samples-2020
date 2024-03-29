﻿using System;
using System.Collections.Generic;

// Dijkstra 法に特化した priority queue を利用します。
namespace AlgorithmLab.Graphs.SPPs.Dijkstra252
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
			var q = new HeapQueue(Vertexes);
			q.AddOrUpdate(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (v == ev) return;
				var vo = Vertexes[v];

				foreach (var (nv, cost) in vo.Edges)
				{
					var nvo = Vertexes[nv];
					var nc = vo.Cost + cost;
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Previous = vo;
					q.AddOrUpdate(nv);
				}
			}
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class HeapQueue
	{
		readonly Vertex[] vs;
		int[] a;
		readonly int[] map;
		int n = 1;

		public HeapQueue(Vertex[] vs)
		{
			this.vs = vs;
			a = new int[4];
			map = new int[vs.Length];
		}

		public int Count => n - 1;
		public int First => a[1];

		public void AddOrUpdate(int v)
		{
			if (map[v] == 0)
			{
				if (n == a.Length) Array.Resize(ref a, a.Length << 1);
				map[a[n] = v] = n++;
			}

			for (int i = map[v]; i > 1 && vs[a[i]].Cost < vs[a[i >> 1]].Cost; i >>= 1)
			{
				(map[a[i]], map[a[i >> 1]]) = (i >> 1, i);
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
		}

		public int Pop()
		{
			var v = a[1];
			map[a[1] = a[--n]] = 1;

			for (int i = 2; i < n; i <<= 1)
			{
				if (i + 1 < n && vs[a[i]].Cost > vs[a[i + 1]].Cost) ++i;
				if (vs[a[i]].Cost >= vs[a[i >> 1]].Cost) break;
				(map[a[i]], map[a[i >> 1]]) = (i >> 1, i);
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
			return v;
		}
	}
}
