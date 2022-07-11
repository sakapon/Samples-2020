using System;
using System.Collections.Generic;

namespace AlgorithmLab.IO
{
	public class AsciiIO
	{
		static bool[] lf = new bool[1 << 7];
		static bool[] sp = new bool[1 << 7];
		static AsciiIO() => lf['\r'] = lf['\n'] = sp['\r'] = sp['\n'] = sp[' '] = true;

		System.IO.Stream si = new System.IO.BufferedStream(Console.OpenStandardInput(), 8192);
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		int b, s;
		void NextValid() { while (sp[b = si.ReadByte()]) ; }
		bool Next() => !sp[b = si.ReadByte()];

		public char Char() { NextValid(); return (char)b; }

		public int Int()
		{
			NextValid();
			if ((s = b) == '-') Next();
			var r = 0;
			do r = r * 10 + (b & ~'0'); while (Next());
			return s == '-' ? -r : r;
		}
		public int[] Int(int n)
		{
			var r = new int[n];
			for (var i = 0; i < n; ++i) r[i] = Int();
			return r;
		}
		public int[][] IntMatrix(int n, int m)
		{
			var r = new int[n][];
			for (var i = 0; i < n; ++i) r[i] = Int(m);
			return r;
		}

		public long Long()
		{
			NextValid();
			if ((s = b) == '-') Next();
			var r = 0L;
			do r = r * 10 + (b & ~'0'); while (Next());
			return s == '-' ? -r : r;
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

		public T Read<T>()
		{
			if (typeof(T) == typeof(int)) return (T)(object)Int();
			if (typeof(T) == typeof(long)) return (T)(object)Long();
			return (T)Convert.ChangeType(String(), typeof(T));
		}
		public T[] Read<T>(int n)
		{
			var r = new T[n];
			for (var i = 0; i < n; ++i) r[i] = Read<T>();
			return r;
		}
		public T[] Read<T>(int n, Func<T> f)
		{
			var r = new T[n];
			for (var i = 0; i < n; ++i) r[i] = f();
			return r;
		}

		public string StringLine()
		{
			int b;
			while (sp[b = si.ReadByte()]) ;
			do sb.Append((char)b); while (!lf[b = si.ReadByte()]);
			var r = sb.ToString();
			sb.Clear();
			return r;
		}

		public IEnumerable<T> EnumerateLine<T>()
		{
			int b;
			do
			{
				while (sp[b = si.ReadByte()]) ;
				do sb.Append((char)b); while (!sp[b = si.ReadByte()]);
				yield return (T)Convert.ChangeType(sb.ToString(), typeof(T));
				sb.Clear();
			}
			while (!lf[b]);
		}

		const string TText = "Yes";
		const string FText = "No";
		public void Write(object o)
		{
			if (o is null) { }
			else if (o is bool b) Console.WriteLine(b ? TText : FText);
			else if (o is string s)
			{
				if (lf[s[^1]]) Console.Write(o);
				else Console.WriteLine(o);
			}
			else Console.WriteLine(o);
		}
	}
}
