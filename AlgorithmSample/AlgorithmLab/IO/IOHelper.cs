using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmLab.IO
{
	public static class IOHelper
	{
		public static AsciiReader In { get; } = new AsciiReader(Console.In);
	}

	public class AsciiReader
	{
		readonly TextReader reader;
		readonly StringBuilder sb = new StringBuilder();
		public AsciiReader(TextReader reader) => this.reader = reader;

		// EOL のとき true
		bool ReadBlock(out string value)
		{
			var eol = false;
			while (true)
			{
				var b = reader.Read();
				if (b == 32 || b == -1) break;
				if (b == 10 || b == 13)
				{
					if (b == 13 && reader.Peek() == 10) reader.Read();
					eol = true;
					break;
				}
				sb.Append((char)b);
			}
			value = sb.ToString();
			sb.Clear();
			return eol;
		}
		bool ReadBlock<T>(out T value)
		{
			var eol = ReadBlock(out var s);
			value = (T)Convert.ChangeType(s, typeof(T));
			return eol;
		}

		public AsciiReader Read(out string value)
		{
			ReadBlock(out value);
			return this;
		}
		public AsciiReader Read<T>(out T value)
		{
			ReadBlock(out value);
			return this;
		}

		public AsciiReader ReadLine(out string value)
		{
			value = reader.ReadLine();
			return this;
		}
		public AsciiReader ReadLine<T>(out T value)
		{
			var s = reader.ReadLine();
			value = (T)Convert.ChangeType(s, typeof(T));
			return this;
		}
		public AsciiReader ReadLine<T>(out T[] values)
		{
			var vs = new List<T>();
			var b = false;
			while (!b)
			{
				b = ReadBlock(out T v);
				vs.Add(v);
			}
			values = vs.ToArray();
			return this;
		}

		public AsciiReader ReadLines<T>(int n, out T[] values)
		{
			values = new T[n];
			for (int i = 0; i < n; ++i)
			{
				ReadLine(out T v);
				values[i] = v;
			}
			return this;
		}
		public AsciiReader ReadLines<T>(int n, out T[][] values)
		{
			values = new T[n][];
			for (int i = 0; i < n; ++i)
			{
				ReadLine(out T[] v);
				values[i] = v;
			}
			return this;
		}
	}
}
