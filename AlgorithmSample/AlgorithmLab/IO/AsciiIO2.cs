using System;
using System.Collections.Generic;
using System.Linq;

// BufferedStream と Long() により高速化します。
namespace AlgorithmLab.IO
{
	public class AsciiIO2
	{
		public static AsciiReader2 In { get; } = new AsciiReader2(Console.OpenStandardInput());
	}

	public class AsciiReader2
	{
		readonly System.IO.Stream si;
		readonly System.Text.StringBuilder sb = new System.Text.StringBuilder();
		public AsciiReader2(System.IO.Stream si) => this.si = new System.IO.BufferedStream(si, 8192);

		int b = -1;
		void ReadBlock(Action action)
		{
			if (b == -1) b = si.ReadByte();
			while (true)
			{
				if (b == 32 || b == 10 || b == -1) break;
				if (b == 13)
				{
					if ((b = si.ReadByte()) != 10) return;
					break;
				}
				action();
				b = si.ReadByte();
			}
			b = -1;
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
			ReadBlock(() => sb.Append((char)b));
			var r = sb.ToString();
			sb.Clear();
			return r;
		}
		public T Value<T>()
		{
			var o =
				typeof(T) == typeof(int) ? Int() :
				typeof(T) == typeof(long) ? Long() :
				typeof(T) == typeof(string) ? String() :
				Convert.ChangeType(String(), typeof(T));
			return (T)o;
		}

		public AsciiReader2 Read<T>(out T value)
		{
			value = Value<T>();
			return this;
		}
		public AsciiReader2 Read<T>(int n, out T[] values)
		{
			values = new T[n];
			for (int i = 0; i < n; ++i) values[i] = Value<T>();
			return this;
		}
		public AsciiReader2 Read<T1, T2>(int n, out (T1, T2)[] values)
		{
			values = new (T1, T2)[n];
			for (int i = 0; i < n; ++i) values[i] = (Value<T1>(), Value<T2>());
			return this;
		}
	}
}
