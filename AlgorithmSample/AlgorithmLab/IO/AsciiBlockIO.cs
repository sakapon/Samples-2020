using System;

// BufferedStream と Long() により高速化します。
// 指定したブロックのみ読み取ります。
// static AsciiBlockReader cin = new AsciiBlockReader(Console.OpenStandardInput());
namespace AlgorithmLab.IO
{
	public class AsciiBlockIO
	{
		public static AsciiBlockReader In { get; } = new AsciiBlockReader(Console.OpenStandardInput());
		public static AsciiBlockWriter Out { get; } = new AsciiBlockWriter(Console.OpenStandardOutput());
	}

	public class AsciiBlockReader
	{
		static readonly bool[] SP = new bool[1 << 7];
		static AsciiBlockReader() => SP['\r'] = SP['\n'] = SP['\t'] = SP[' '] = true;

		readonly System.IO.Stream si;
		readonly System.Text.StringBuilder sb = new System.Text.StringBuilder();
		public AsciiBlockReader(System.IO.Stream si) => this.si = new System.IO.BufferedStream(si, 8192);

		int b = -1;
		void ReadOne()
		{
			while ((b = si.ReadByte()) != -1 && SP[b]) ;
		}
		void ReadBlock(Action action)
		{
			while ((b = si.ReadByte()) != -1 && SP[b]) ;
			if (b == -1) return;
			do action();
			while ((b = si.ReadByte()) != -1 && !SP[b]);
		}

		public int Int() => (int)Long();
		public long Long()
		{
			var sign = true;
			var r = 0L;
			ReadBlock(() =>
			{
				if (b == '-') sign = false;
				else r = r * 10 + (b & ~'0');
			});
			return sign ? r : -r;
		}
		public string String()
		{
			sb.Clear();
			ReadBlock(() => sb.Append((char)b));
			return sb.ToString();
		}
		public char Char()
		{
			ReadOne();
			return (char)b;
		}
		public T Value<T>()
		{
			var o =
				typeof(T) == typeof(int) ? Int() :
				typeof(T) == typeof(long) ? Long() :
				typeof(T) == typeof(string) ? String() :
				typeof(T) == typeof(char) ? Char() :
				Convert.ChangeType(String(), typeof(T));
			return (T)o;
		}
		public T[] Values<T>(int n)
		{
			var a = new T[n];
			for (int i = 0; i < n; ++i) a[i] = Value<T>();
			return a;
		}

		public void Read<T>(out T v) => v = Value<T>();
		public void Read<T1, T2>(out (T1, T2) v) => v = (Value<T1>(), Value<T2>());
		public void Read<T>(int n, out T[] v) => v = Values<T>(n);
		public void Read<T1, T2>(int n, out (T1, T2)[] v)
		{
			v = new (T1, T2)[n];
			for (int i = 0; i < n; ++i) v[i] = (Value<T1>(), Value<T2>());
		}
		public void Read<T>(int n1, int n2, out T[][] v)
		{
			v = new T[n1][];
			for (int i = 0; i < n1; ++i) v[i] = Values<T>(n2);
		}
	}

	public class AsciiBlockWriter
	{
		readonly System.IO.Stream so;
		public AsciiBlockWriter(System.IO.Stream so) => this.so = new System.IO.BufferedStream(so, 8192);

		public void Flush() => so.Flush();
		public void Write(string s, bool lf = true)
		{
			foreach (var c in s) so.WriteByte((byte)c);
			if (lf) so.WriteByte(10);
		}
		public void Write(bool b, bool lf = true) => Write(b ? "Yes" : "No", lf);
		public void Write<T>(T v, bool lf = true)
		{
			if (v is bool b) Write(b, lf);
			else Write(v.ToString(), lf);
		}

		public void WriteLine<T1, T2>(T1 v1, T2 v2)
		{
			Write(v1, false); so.WriteByte(32);
			Write(v2, false); so.WriteByte(10);
		}
		public void WriteLine<T>(T[] vs)
		{
			for (int i = 0; i < vs.Length; ++i)
			{
				if (i != 0) so.WriteByte(32);
				Write(vs[i].ToString(), false);
			}
			so.WriteByte(10);
		}
		public void WriteLines<T>(T[] vs)
		{
			foreach (var v in vs) Write(v.ToString());
		}
	}
}
