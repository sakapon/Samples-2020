using System;

namespace AlgorithmLib.Maths
{
	public static class Combinatorics
	{
		static int[] Range(int n)
		{
			if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));

			var v = new int[n];
			for (int i = 0; i < n; ++i) v[i] = i;
			return v;
		}

		// 0-indexed nPr
		public static void Permutation(int n, int r, Action<int[]> action) => Permutation(Range(n), r, action);

		public static void Permutation<T>(T[] values, int r, Action<T[]> action)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (r < 0 || values.Length < r) throw new ArgumentOutOfRangeException(nameof(r));
			if (action == null) throw new ArgumentNullException(nameof(action));

			var n = values.Length;
			var p = new T[r];
			var u = new bool[n];

			if (r > 0) Dfs(0);
			else action(p);

			void Dfs(int i)
			{
				var i2 = i + 1;
				for (int j = 0; j < n; ++j)
				{
					if (u[j]) continue;
					p[i] = values[j];
					u[j] = true;

					if (i2 < r) Dfs(i2);
					else action(p);

					u[j] = false;
				}
			}
		}

		// 0-indexed nCr
		public static void Combination(int n, int r, Action<int[]> action) => Combination(Range(n), r, action);

		public static void Combination<T>(T[] values, int r, Action<T[]> action)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (r < 0 || values.Length < r) throw new ArgumentOutOfRangeException(nameof(r));
			if (action == null) throw new ArgumentNullException(nameof(action));

			var n = values.Length;
			var p = new T[r];

			if (r > 0) Dfs(0, 0);
			else action(p);

			void Dfs(int i, int j0)
			{
				var i2 = i + 1;
				for (int j = j0; j < n; ++j)
				{
					p[i] = values[j];

					if (i2 < r) Dfs(i2, j + 1);
					else action(p);
				}
			}
		}

		// 0-indexed Power
		public static void Power(int n, int r, Action<int[]> action) => Power(Range(n), r, action);

		public static void Power<T>(T[] values, int r, Action<T[]> action)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (r < 0) throw new ArgumentOutOfRangeException(nameof(r));
			if (action == null) throw new ArgumentNullException(nameof(action));

			var n = values.Length;
			var p = new T[r];

			if (r > 0) Dfs(0);
			else action(p);

			void Dfs(int i)
			{
				var i2 = i + 1;
				for (int j = 0; j < n; ++j)
				{
					p[i] = values[j];

					if (i2 < r) Dfs(i2);
					else action(p);
				}
			}
		}
	}
}
