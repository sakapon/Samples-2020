using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF602;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataTrees.UF
{
	[TestClass]
	public class UnionFind602Test
	{
		[TestMethod]
		public void Distance()
		{
			var n = 13;
			var uf = new UnionFind<int>(n, 0, v => -v, (u, v) => u + v);

			uf.Union(6, 8, 2);
			uf.Union(9, 11, 2);
			uf.Union(8, 11, 3);
			uf.Union(5, 1, -4);
			uf.Union(4, 12, 8);
			uf.Union(5, 4, -1);
			uf.Union(9, 12, 3);

			for (int i = 0; i < n; i++)
			{
				var root = uf.Find(i);
				Assert.AreEqual(i - root.Key, uf[i]);
			}
		}

		[TestMethod]
		public void Distance_Random()
		{
			var random = new Random();
			var n = 100000;
			var uf = new UnionFind<int>(n, 0, v => -v, (u, v) => u + v);

			for (int i = 0; i < 100000; i++)
			{
				var x = random.Next(n);
				var y = random.Next(n);
				uf.Union(x, y, y - x);
			}

			for (int i = 0; i < n; i++)
			{
				var root = uf.Find(i);
				Assert.AreEqual(i - root.Key, uf[i]);
			}
		}
	}
}
