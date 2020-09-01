using System;
using System.Collections.Generic;

namespace OperatorsLib.Classes
{
	public static class QueueEx
	{
		// コレクション初期化子の例。
		// IEnumerable インターフェイスを実装している型が対象です。
		// 引数の型に制限はありません。
		public static void Add<T>(this Queue<T> q, int i)
		{
		}

		public static void Add<T>(this Queue<T> q, int i, double d)
		{
		}

		public static void Add<T>(this Queue<T> q, int i, string s)
		{
		}

		public static void Add<T>(this Queue<T> q, int i, double d, string s)
		{
		}
	}
}
