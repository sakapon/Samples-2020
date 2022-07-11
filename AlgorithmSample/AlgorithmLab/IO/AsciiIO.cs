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

		public int Int() => (int)Long();
		public int[] Int(int n)
		{
			var r = new int[n];
			for (var i = 0; i < n; ++i) r[i] = (int)Long();
			return r;
		}
		public int[][] Int(int n, int m) => Array.ConvertAll(new bool[n], _ => Int(m));

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
		public long[][] Long(int n, int m) => Array.ConvertAll(new bool[n], _ => Long(m));

		public string String()
		{
			NextValid();
			do sb.Append((char)b); while (Next());
			var r = sb.ToString();
			sb.Clear();
			return r;
		}

		public T Read<T>()
		{
			var o =
				typeof(T) == typeof(int) ? Int() :
				typeof(T) == typeof(long) ? Long() :
				typeof(T) == typeof(string) ? String() :
				typeof(T) == typeof(char) ? Char() :
				Convert.ChangeType(String(), typeof(T));
			return (T)o;
		}
		public T[] Read<T>(int n)
		{
			var r = new T[n];
			for (var i = 0; i < n; ++i) r[i] = Read<T>();
			return r;
		}
		public T[][] Read<T>(int n, int m) => Array.ConvertAll(new bool[n], _ => Read<T>(m));
		public T[] Read<T>(int n, Converter<bool, T> f) => Array.ConvertAll(new bool[n], f);

		public (T1, T2) Tuple<T1, T2>() => (Read<T1>(), Read<T2>());
		public (T1, T2, T3) Tuple<T1, T2, T3>() => (Read<T1>(), Read<T2>(), Read<T3>());
		public (T, T) Tuple2<T>() => (Read<T>(), Read<T>());
		public (T, T, T) Tuple3<T>() => (Read<T>(), Read<T>(), Read<T>());

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
