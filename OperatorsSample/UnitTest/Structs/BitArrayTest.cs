using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class BitArrayTest
	{
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
			Assert.AreEqual(10, (int)b);

			b[5] = true;
			Assert.AreEqual(true, b[5]);
			Assert.AreEqual(42, (int)b);
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
		public void Parse()
		{
			var b = BitArray.Parse("65535");
			Assert.AreEqual(65535, b);
			Assert.AreEqual("65535", b.ToString());
		}

		[TestMethod]
		public void BitSwitches()
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
	}
}
