﻿// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_A
// 容量を固定します。
namespace AlgorithmLab.Collections.Arrays101
{
	public class ArrayList<T>
	{
		T[] a;
		int n;

		public ArrayList(int capacity) => a = new T[capacity];

		public int Count => n;
		public T this[int i]
		{
			get => a[i];
			set => a[i] = value;
		}
		public T Last
		{
			get => a[n - 1];
			set => a[n - 1] = value;
		}

		public void Clear() => n = 0;
		public void Add(T item) => a[n++] = item;
		public T Pop() => a[--n];
	}
}
