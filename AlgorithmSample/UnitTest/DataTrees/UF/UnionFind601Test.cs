using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF601;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataTrees.UF
{
	[TestClass]
	public class UnionFind601Test
	{
		[TestMethod]
		public void Distance()
		{
			var n = 13;
			var uf = new UnionFind<int>(n, 0, (x, y) => x + y, (x, y) => y - x);

			uf.Union(6, 8);
			uf.Union(9, 11);
			uf.Union(8, 11);
			uf.Union(5, 1);
			uf.Union(4, 12);
			uf.Union(5, 4);
			uf.Union(9, 12);

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
			var uf = new UnionFind<int>(n, 0, (x, y) => x + y, (x, y) => y - x);

			for (int i = 0; i < 100000; i++)
			{
				uf.Union(random.Next(n), random.Next(n));
			}

			for (int i = 0; i < n; i++)
			{
				var root = uf.Find(i);
				Assert.AreEqual(i - root.Key, uf[i]);
			}
		}
	}
}
