using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLib.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Maths
{
	[TestClass]
	public class CombinatoricsTest
	{
		[TestMethod]
		public void Permutation()
		{
			int n = 10, r = 9, c = 0;
			var nPr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y);
			Combinatorics.Permutation(n, r, p => ++c);
			Assert.AreEqual(nPr, c);
		}

		[TestMethod]
		public void Permutation_Char()
		{
			var cs = "1234".ToCharArray();
			var expected = Enumerable.Range(0, 10000).Select(i => i.ToString()).Where(s => cs.All(c => s.Contains(c))).ToArray();
			var actual = new List<string>();
			Combinatorics.Permutation(cs, cs.Length, p => actual.Add(new string(p)));
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Permutation_r()
		{
			for (int n = 4, r = 0; r <= n; r++, Console.WriteLine())
				Combinatorics.Permutation(n, r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Permutation(new[] { 9, 7, 5, 3 }, 3, p => Console.WriteLine(string.Join(" ", p)));
		}

		[TestMethod]
		public void Combination()
		{
			int n = 60, r = 5, c = 0;
			var nCr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y) / Enumerable.Range(1, r).Aggregate((x, y) => x * y);
			Combinatorics.Combination(n, r, p => ++c);
			Assert.AreEqual(nCr, c);
		}

		[TestMethod]
		public void Combination_Char()
		{
			var cs = "ABCDEF1234567890".ToCharArray();
			var expected = new List<string>();
			for (int i = 0; i < cs.Length; i++)
				for (int j = i + 1; j < cs.Length; j++)
					expected.Add($"{cs[i]}{cs[j]}");
			var actual = new List<string>();
			Combinatorics.Combination(cs, 2, p => actual.Add(new string(p)));
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Combination_r()
		{
			for (int n = 6, r = 0; r <= n; r++, Console.WriteLine())
				Combinatorics.Combination(n, r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Combination(new[] { 9, 7, 5, 3, 1 }, 3, p => Console.WriteLine(string.Join(" ", p)));
		}

		[TestMethod]
		public void Power()
		{
			int n = 10, r = 7, c = 0;
			var pow = Enumerable.Repeat(n, r).Aggregate((x, y) => x * y);
			Combinatorics.Power(n, r, p => ++c);
			Assert.AreEqual(pow, c);
		}

		[TestMethod]
		public void Power_Char()
		{
			var cs = "ABCDEF1234567890".ToCharArray();
			var expected = new List<string>();
			for (int i = 0; i < cs.Length; i++)
				for (int j = 0; j < cs.Length; j++)
					expected.Add($"{cs[i]}{cs[j]}");
			var actual = new List<string>();
			Combinatorics.Power(cs, 2, p => actual.Add(new string(p)));
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Power_r()
		{
			for (int n = 3, r = 0; r <= 4; r++, Console.WriteLine())
				Combinatorics.Power(n, r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Power(new[] { 9, 7, 5, 3, 1 }, 2, p => Console.WriteLine(string.Join(" ", p)));
		}
	}
}
