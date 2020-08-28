using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class TernaryTest
	{
		[TestMethod]
		public void Conditions()
		{
			Ternary t = true;
			Ternary n = null;
			Ternary f = false;

			if (t) { }
			else Assert.Fail();
			if (!t) Assert.Fail();
			else { }
			if (f) Assert.Fail();
			else { }
			if (!f) { }
			else Assert.Fail();
			if (n) Assert.Fail();
			else { }
			if (!n) Assert.Fail();
			else { }

			Assert.AreEqual(1, t ? 1 : 0);
			Assert.AreEqual(0, !t ? 1 : 0);
			Assert.AreEqual(0, f ? 1 : 0);
			Assert.AreEqual(1, !f ? 1 : 0);
			Assert.AreEqual(0, n ? 1 : 0);
			Assert.AreEqual(0, !n ? 1 : 0);
		}

		[TestMethod]
		public void Logics()
		{
			Ternary t = true;
			Ternary n = null;
			Ternary f = false;
			var b = new[] { t, n, f };

			for (int i = 0; i < 3; i++)
				Assert.AreEqual(!b[i].Value, (!b[i]).Value);

			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(b[i].Value & b[j].Value, (b[i] & b[j]).Value);
					Assert.AreEqual(b[i].Value ^ b[j].Value, (b[i] ^ b[j]).Value);
					Assert.AreEqual(b[i].Value | b[j].Value, (b[i] | b[j]).Value);

					Assert.AreEqual(!(b[i].Value ^ b[j].Value), (b[i] == b[j]).Value);
					Assert.AreEqual(b[i].Value ^ b[j].Value, (b[i] != b[j]).Value);
				}

			// bool? で && および || は利用できません。
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(b[i].Value & b[j].Value, (b[i] && b[j]).Value);
					Assert.AreEqual(b[i].Value | b[j].Value, (b[i] || b[j]).Value);
				}
		}
	}
}
