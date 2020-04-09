using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections
{
	public static class BinarySearch
	{
		// 条件 f を満たす最初の値を探索します。
		// f(r - 1) が false のとき、r を返します。
		// f(r) は評価されません。
		public static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r)
				if (f(m = l + (r - l - 1) / 2)) r = m;
				else l = m + 1;
			return r;
		}

		// 条件 f を満たす最後の値を探索します。
		// f(l + 1) が false のとき、l を返します。
		// f(l) は評価されません。
		public static int Last(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r)
				if (f(m = r - (r - l - 1) / 2)) l = m;
				else r = m - 1;
			return l;
		}

		public static double First(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0)
				if (f(m = l + (r - l) / 2)) r = m;
				else l = m;
			return r;
		}

		public static double Last(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0)
				if (f(m = r - (r - l) / 2)) l = m;
				else r = m;
			return l;
		}

		#region For List

		// 指定された値よりも大きい値を持つ最初のインデックスを探索します (昇順の場合)。
		// これは、挿入先のインデックスを意味します。
		public static int FirstGt(IList<int> a, int v)
		{
			int l = 0, r = a.Count, m;
			while (l < r)
				if (a[m = l + (r - l - 1) / 2] > v) r = m;
				else l = m + 1;
			return r;
		}

		// 指定された値よりも大きい値を持つ最後のインデックスを探索します (降順の場合)。
		public static int LastGt(IList<int> a, int v)
		{
			int l = -1, r = a.Count - 1, m;
			while (l < r)
				if (a[m = r - (r - l - 1) / 2] > v) l = m;
				else r = m - 1;
			return l;
		}

		// (昇順の場合)
		public static int FirstGeq(IList<int> a, int v) => First(0, a.Count, x => a[x] >= v);
		// (昇順の場合)
		public static int LastLeq(IList<int> a, int v) => Last(-1, a.Count - 1, x => a[x] <= v);

		#endregion
	}
}
