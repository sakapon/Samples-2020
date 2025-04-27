using System;

// BufferedStream と Long() により高速化します。
// 指定したブロックのみ読み取ります。
// static AsciiBlockReader cin = new AsciiBlockReader(Console.OpenStandardInput());
namespace AlgorithmLab.IO
{
	public class AsciiBlockIO
	{
		public static AsciiBlockReader In { get; } = new AsciiBlockReader(Console.OpenStandardInput());
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
}
