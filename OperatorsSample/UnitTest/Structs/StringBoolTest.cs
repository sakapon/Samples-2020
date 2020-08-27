﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Structs;

namespace UnitTest.Structs
{
	[TestClass]
	public class StringBoolTest
	{
		[TestMethod]
		public void Conditions()
		{
			StringBool t = "true";
			StringBool n = "force";
			StringBool f = "false";

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
			StringBool t = "true";
			StringBool n = "force";
			StringBool f = "false";
			var s = new[] { t, n, f };
			var b = new bool?[] { true, null, false };

			for (int i = 0; i < 3; i++)
				Assert.AreEqual(!b[i], (bool?)!s[i]);

			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(b[i] & b[j], (bool?)(s[i] & s[j]));
					Assert.AreEqual(b[i] ^ b[j], (bool?)(s[i] ^ s[j]));
					Assert.AreEqual(b[i] | b[j], (bool?)(s[i] | s[j]));
				}

			// bool? で && および || は利用できません。
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(b[i] & b[j], (bool?)(s[i] && s[j]));
					Assert.AreEqual(b[i] | b[j], (bool?)(s[i] || s[j]));
				}
		}
	}
}
