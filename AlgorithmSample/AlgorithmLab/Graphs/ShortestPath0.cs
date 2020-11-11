﻿using System;
using System.Collections.Generic;
using AlgorithmLab.DataTrees;

namespace AlgorithmLab.Graphs
{
	// 配列を用いた基本的な実装です。
	// Test: https://judge.yosupo.jp/problem/shortest_path
	// Test: https://codeforces.com/contest/20/problem/C
	public static class ShortestPath0
	{
		/// <summary>
		/// Dijkstra 法により、始点からの最短路を求めます。
		/// </summary>
		/// <param name="vertexesCount">頂点の個数。これ未満の値を ID として使用できます。</param>
		/// <param name="edges">edge: { from, to, cost }</param>
		/// <param name="directed">有向グラフかどうかを表す値。</param>
		/// <param name="startVertexId">始点の ID。</param>
		/// <param name="endVertexId">終点の ID。終点を指定しない場合、-1。</param>
		/// <returns>最小コストおよび入辺。到達不可能の場合、最小コストは <see cref="long.MaxValue"/>、入辺は <see langword="null"/>。</returns>
		/// <remarks>
		/// グラフの有向性、連結性、多重性、開閉を問いません。したがって、1-indexed でも利用できます。<br/>
		/// 辺のコストは非負でなければなりません。
		/// </remarks>
		public static (long[] minCosts, int[][] inEdges) Dijkstra(int vertexesCount, int[][] edges, bool directed, int startVertexId, int endVertexId = -1)
		{
			if (edges == null) throw new ArgumentNullException(nameof(edges));

			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int[]>());
			foreach (var e in edges)
			{
				// 入力チェックは省略。
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inEdges = new int[vertexesCount][];
			var q = Heap<int>.CreateWithKey(v => costs[v]);
			costs[startVertexId] = 0;
			q.Push(startVertexId);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == endVertexId) break;
				if (costs[v] < c) continue;

				foreach (var e in map[v])
				{
					var nc = costs[v] + e[2];
					if (costs[e[1]] <= nc) continue;
					costs[e[1]] = nc;
					inEdges[e[1]] = e;
					q.Push(e[1]);
				}
			}
			return (costs, inEdges);
		}

		// priority queue ではなく、queue を使うほうが速いことがあります。
		[Obsolete("最悪計算量は O(VE) です。")]
		public static (long[] minCosts, int[][] inEdges) Dijklmna(int vertexesCount, int[][] edges, bool directed, int startVertexId, int endVertexId = -1)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int[]>());
			foreach (var e in edges)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inEdges = new int[vertexesCount][];
			var q = new Queue<int>();
			costs[startVertexId] = 0;
			q.Enqueue(startVertexId);

			while (q.Count > 0)
			{
				var v = q.Dequeue();

				foreach (var e in map[v])
				{
					var nc = costs[v] + e[2];
					if (costs[e[1]] <= nc) continue;
					costs[e[1]] = nc;
					inEdges[e[1]] = e;
					q.Enqueue(e[1]);
				}
			}
			return (costs, inEdges);
		}

		public static int[] GetPathVertexes(int[][] inEdges, int endVertexId)
		{
			var path = new Stack<int>();
			path.Push(endVertexId);
			for (var e = inEdges[endVertexId]; e != null; e = inEdges[e[0]])
				path.Push(e[0]);
			return path.ToArray();
		}

		public static int[][] GetPathEdges(int[][] inEdges, int endVertexId)
		{
			var path = new Stack<int[]>();
			for (var e = inEdges[endVertexId]; e != null; e = inEdges[e[0]])
				path.Push(e);
			return path.ToArray();
		}
	}
}
