using System;

namespace AlgorithmLab.IO
{
	public static class ConsoleMethods
	{
		static string Read() => Console.ReadLine();
		static T To<T>(string s) => (T)Convert.ChangeType(s, typeof(T));
		static void Read<T>(out T v) => v = To<T>(Read());
		static void Read<T1, T2>(out T1 v1, out T2 v2)
		{
			var p = Read().Split();
			(v1, v2) = (To<T1>(p[0]), To<T2>(p[1]));
		}
		static void Read<T>(out T[] v) => v = Array.ConvertAll(Read().Split(), To<T>);
		static void Reads<T>(int n, out T[] v) => v = Array.ConvertAll(new bool[n], _ => To<T>(Read()));
		static void Reads<T1, T2>(int n, out (T1, T2)[] v) => v = Array.ConvertAll(new bool[n], _ => { Read(out T1 v1, out T2 v2); return (v1, v2); });
	}
}
