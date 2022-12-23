using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Graphs.SPPs.Dijkstra253;

namespace OnlineTest.Graphs.SPPs
{
	// Test: https://judge.yosupo.jp/problem/shortest_path
	class YJLC_SP
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m, s, t) = Read4();
			var es = Array.ConvertAll(new bool[m], _ => Read3());

			var graph = new Dijkstra(n, es, false);
			graph.Execute(s, t);
			var ev = graph[t];
			if (!ev.IsConnected) return -1;

			var path = ev.GetPathEdges();
			return $"{ev.Cost} {path.Length}\n" + string.Join("\n", path.Select(e => $"{e.Item1} {e.Item2}"));
		}
	}
}
