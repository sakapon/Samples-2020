using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	public static class TestHelper
	{
		public static void AssertNearlyEqual(double expected, double actual, int digits = 9) =>
			Assert.AreEqual(0.0, Math.Round(expected - actual, digits));

		public static void MeasureTime(Action action)
		{
			var sw = Stopwatch.StartNew();
			action();
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
		}

		public static T MeasureTime<T>(Func<T> func)
		{
			var sw = Stopwatch.StartNew();
			var result = func();
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			return result;
		}
	}
}
