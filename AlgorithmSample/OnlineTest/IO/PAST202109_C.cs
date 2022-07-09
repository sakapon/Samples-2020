using System;
using System.Linq;

namespace OnlineTest.IO
{
	// Test: https://atcoder.jp/contests/past202109-open/tasks/past202109_c
	class PAST202109_C
	{
		static AsciiIO io = new AsciiIO();
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = io.Int();
			var x = io.Int();
			var a = io.Int(n);

			return a.Count(v => v == x);
		}
	}

	class AsciiIO
	{
		static bool[] sp = new bool[1 << 7];
		static AsciiIO() => sp['\r'] = sp['\n'] = sp[' '] = true;

		System.IO.Stream si = new System.IO.BufferedStream(Console.OpenStandardInput(), 8192);
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		public int Int()
		{
			int b;
			while (sp[b = si.ReadByte()]) ;
			var r = 0;
			do r = r * 10 + (b & ~'0'); while (!sp[b = si.ReadByte()]);
			return r;
		}
		public int[] Int(int n)
		{
			var r = new int[n];
			for (var i = 0; i < n; ++i) r[i] = Int();
			return r;
		}

		public long Long()
		{
			int b;
			while (sp[b = si.ReadByte()]) ;
			var r = 0L;
			do r = r * 10 + (b & ~'0'); while (!sp[b = si.ReadByte()]);
			return r;
		}
		public long[] Long(int n)
		{
			var r = new long[n];
			for (var i = 0; i < n; ++i) r[i] = Long();
			return r;
		}

		public string String()
		{
			int b;
			while (sp[b = si.ReadByte()]) ;
			do sb.Append((char)b); while (!sp[b = si.ReadByte()]);
			var r = sb.ToString();
			sb.Clear();
			return r;
		}
		public string[] String(int n)
		{
			var r = new string[n];
			for (var i = 0; i < n; ++i) r[i] = String();
			return r;
		}

		public T Read<T>() => (T)Convert.ChangeType(String(), typeof(T));
		public T[] Read<T>(int n)
		{
			var r = new T[n];
			for (var i = 0; i < n; ++i) r[i] = Read<T>();
			return r;
		}
	}
}
