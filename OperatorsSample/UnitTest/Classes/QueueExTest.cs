using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorsLib.Classes;

namespace UnitTest.Classes
{
	[TestClass]
	public class QueueExTest
	{
		[TestMethod]
		public void Initializer()
		{
			var q = new Queue<bool>
			{
				123,
				{ 1, 23, "abc" },
				{ 1, "abc" },
				{ 1, 23 },
			};
		}
	}
}
