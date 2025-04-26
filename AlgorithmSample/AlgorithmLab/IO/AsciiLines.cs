using System;

// BufferedStream により高速化します (が、ConsoleMethods と変わりません)。
namespace AlgorithmLab.IO
{
	public class AsciiLineReader
	{
		static T To<T>(string s)
		{
			var o =
				typeof(T) == typeof(int) ? int.Parse(s) :
				typeof(T) == typeof(long) ? long.Parse(s) :
				typeof(T) == typeof(string) ? s :
				Convert.ChangeType(s, typeof(T));
			return (T)o;
		}

		readonly System.IO.Stream si;
		readonly System.Text.StringBuilder sb = new System.Text.StringBuilder();
		public AsciiLineReader(System.IO.Stream si) => this.si = new System.IO.BufferedStream(si, 8192);

		public string Line()
		{
			sb.Clear();
			int b;
			while ((b = si.ReadByte()) != '\n') if (b != '\r') sb.Append((char)b);
			return sb.ToString();
		}

		public void Read<T>(out T v) => v = To<T>(Line());
		public void Read<T1, T2>(out T1 v1, out T2 v2)
		{
			var p = Line().Split();
			(v1, v2) = (To<T1>(p[0]), To<T2>(p[1]));
		}
		public void Read<T>(out T[] v) => v = Array.ConvertAll(Line().Split(), To<T>);

		public void Reads<T>(int n, out T[] v) => v = Array.ConvertAll(new bool[n], _ => To<T>(Line()));
		public void Reads<T1, T2>(int n, out (T1, T2)[] v) => v = Array.ConvertAll(new bool[n], _ => { Read(out T1 v1, out T2 v2); return (v1, v2); });
		public void Reads<T>(int n, out T[][] v) => v = Array.ConvertAll(new bool[n], _ => { Read(out T[] u); return u; });
	}
}
