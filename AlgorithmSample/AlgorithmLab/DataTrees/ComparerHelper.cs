﻿using System;
using System.Collections.Generic;

namespace AlgorithmLab.DataTrees
{
	public static class ComparerHelper
	{
		public static IComparer<T> GetDefault<T>()
		{
			// カルチャに依存しない場合に高速化します。
			if (typeof(T) == typeof(string)) return (IComparer<T>)StringComparer.Ordinal;
			return Comparer<T>.Default;
		}

		public static IComparer<T> ToDescending<T>(this IComparer<T> c)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));
			return Comparer<T>.Create((x, y) => c.Compare(y, x));
		}
	}

	// クラスに型引数を指定することで、Create メソッドを呼び出すときに型引数 <T, Tkey> の指定を省略できます。
	public static class ComparerHelper<T>
	{
		public static IComparer<T> Create(bool descending = false)
		{
			var c = ComparerHelper.GetDefault<T>();
			return descending ? c.ToDescending() : c;
		}

		public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
			var c = ComparerHelper<TKey>.Create(descending);
			return Comparer<T>.Create((x, y) => c.Compare(keySelector(x), keySelector(y)));
		}

		public static IComparer<T> Create<TKey1, TKey2>(Func<T, TKey1> keySelector1, bool descending1, Func<T, TKey2> keySelector2, bool descending2)
		{
			if (keySelector1 == null) throw new ArgumentNullException(nameof(keySelector1));
			if (keySelector2 == null) throw new ArgumentNullException(nameof(keySelector2));
			var c1 = ComparerHelper<TKey1>.Create(descending1);
			var c2 = ComparerHelper<TKey2>.Create(descending2);
			return Comparer<T>.Create((x, y) =>
			{
				var d = c1.Compare(keySelector1(x), keySelector1(y));
				if (d != 0) return d;
				return c2.Compare(keySelector2(x), keySelector2(y));
			});
		}
	}
}
