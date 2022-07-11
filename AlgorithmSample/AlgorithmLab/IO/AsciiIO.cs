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
		public int[] Int(int n) => Read(n, () => Int());
		public int[][] Int(int n, int m) => Read(n, () => Int(m));
		public (int, int) Int2() => (Int(), Int());
		public (int, int, int) Int3() => (Int(), Int(), Int());

		public long Long()
		{
			NextValid();
			if ((s = b) == '-') Next();
			var r = 0L;
			do r = r * 10 + (b & ~'0'); while (Next());
			return s == '-' ? -r : r;
		}
		public long[] Long(int n) => Read(n, () => Long());
		public long[][] Long(int n, int m) => Read(n, () => Long(m));
		public (long, long) Long2() => (Long(), Long());
		public (long, long, long) Long3() => (Long(), Long(), Long());

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
		public T[] Read<T>(int n, Func<T> f) { var r = new T[n]; for (var i = 0; i < n; ++i) r[i] = f(); return r; }
		public T[] Read<T>(int n) => Read(n, () => Read<T>());
		public T[][] Read<T>(int n, int m) => Read(n, () => Read<T>(m));

		public (T, T) Read2<T>() => (Read<T>(), Read<T>());
		public (T, T, T) Read3<T>() => (Read<T>(), Read<T>(), Read<T>());
		public (T1, T2) Tuple<T1, T2>() => (Read<T1>(), Read<T2>());
		public (T1, T2, T3) Tuple<T1, T2, T3>() => (Read<T1>(), Read<T2>(), Read<T3>());

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
