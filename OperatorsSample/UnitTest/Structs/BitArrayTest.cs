using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;
using BArray = System.Collections.BitArray;

namespace UnitTest.Structs
{
	[TestClass]
	public class BitArrayTest
	{
		[TestMethod]
		public void Parse()
		{
			BitArray b1 = 65535;
			Assert.AreEqual("65535", b1.ToString());

			var b2 = BitArray.Parse(b1.ToString());
			Assert.AreEqual(65535, (int)b2);
			Assert.AreEqual(b1, b2);
		}

		[TestMethod]
		public void Indexer()
		{
			BitArray b = 10;
			Assert.AreEqual(false, b[0]);
			Assert.AreEqual(true, b[1]);
			Assert.AreEqual(false, b[2]);
			Assert.AreEqual(true, b[3]);
			Assert.AreEqual(false, b[4]);
			Assert.AreEqual(false, b[5]);
			Assert.AreEqual(false, b[^27]);
			Assert.AreEqual(10, (int)b);

			b[5] = true;
			Assert.AreEqual(true, b[5]);
			Assert.AreEqual(true, b[^27]);
			Assert.AreEqual(42, (int)b);
		}

		[TestMethod]
		public void Indexer_Index()
		{
			BitArray b = 1000000007;
			var expected = Enumerable.Range(0, 32).Select(i => b[i]).ToArray();
			var actual = Enumerable.Range(1, 32).Select(i => b[^i]).Reverse().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Initializer()
		{
			var b = new BitArray
			{
				[3] = true,
				[6] = true,
			};
			Assert.AreEqual(false, b[2]);
			Assert.AreEqual(true, b[3]);
			Assert.AreEqual(true, b[6]);
			Assert.AreEqual(false, b[7]);
			Assert.AreEqual(72, b);
			Assert.AreEqual(72, b.Value);
		}

		[TestMethod]
		public void BitSearch()
		{
			var n = 8;
			var n2 = 1 << n;

			// bit 全探索
			for (BitArray b = 0; b.Value < n2; b++)
			{
				for (int i = 0; i < n; i++)
				{
					// b[i] の真偽による何らかの処理
					Console.Write(b[i] ? 1 : 0);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void BitSearch_BitArray()
		{
			var n = 8;
			var n2 = 1 << n;

			for (int x = 0; x < n2; x++)
			{
				var b = new BArray(new[] { x });
				for (int i = 0; i < n; i++)
				{
					Console.Write(b[i] ? 1 : 0);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void BitSearch_BitVector32()
		{
			var n = 8;
			var n2 = 1 << n;

			for (int x = 0; x < n2; x++)
			{
				var b = new BitVector32(x);
				for (int i = 0; i < n; i++)
				{
					Console.Write(b[1 << i] ? 1 : 0);
				}
				Console.WriteLine();
			}
		}
	}
}
