using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DataTrees
{
	[TestClass]
	public class BstPriorityQueueTest
	{
		[TestMethod]
		public void Distinct()
		{
			var n = 100000;
			var a = RandomHelper.CreateData(n);
			var expected = a.Distinct().ToArray();

			var actual = new List<int>();
			var q = new DistinctPriorityQueue<int>();
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			Array.Sort(expected);
			CollectionAssert.AreEqual(expected, actual);

			actual.Clear();
			q = new DistinctPriorityQueue<int>(ComparerHelper<int>.Create(true));
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			Array.Reverse(expected);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Bst()
		{
			var n = 100000;
			var a = RandomHelper.CreateData(n);
			var expected = (int[])a.Clone();

			var actual = new List<int>();
			var q = new BstPriorityQueue<int>();
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			Array.Sort(expected);
			CollectionAssert.AreEqual(expected, actual);

			actual.Clear();
			q = new BstPriorityQueue<int>(ComparerHelper<int>.Create(true));
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			Array.Reverse(expected);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Keyed_1()
		{
			var n = 100000;
			var a = RandomHelper.CreateData(n);

			var actual = new List<int>();
			var q = new KeyedPriorityQueue<int, int>(x => x / 10);
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			var expected = a.OrderBy(x => x / 10).ToArray();
			CollectionAssert.AreEqual(expected, actual);

			actual.Clear();
			q = new KeyedPriorityQueue<int, int>(x => x / 10, ComparerHelper<int>.Create(true));
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			expected = a.OrderByDescending(x => x / 10).ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Keyed_2()
		{
			var n = 100000;
			var a = RandomHelper.CreateData(n);

			var actual = new List<int>();
			var q = new KeyedPriorityQueue<int, int>(x => x, ComparerHelper<int>.Create(x => x / 10));
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			var expected = a.OrderBy(x => x / 10).ToArray();
			CollectionAssert.AreEqual(expected, actual);

			actual.Clear();
			q = new KeyedPriorityQueue<int, int>(x => x, ComparerHelper<int>.Create(x => x / 10, true));
			foreach (var x in a) q.Push(x);
			while (q.Count > 0) actual.Add(q.Pop());

			expected = a.OrderByDescending(x => x / 10).ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
