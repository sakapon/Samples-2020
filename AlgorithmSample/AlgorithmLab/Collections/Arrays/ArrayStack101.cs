// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_A
// 容量を固定します。
namespace AlgorithmLab.Collections.Arrays.ArrayStack101
{
	public class ArrayStack<T>
	{
		T[] a;
		int n;

		public ArrayStack(int capacity) => a = new T[capacity];

		public int Count => n;
		public T this[int i]
		{
			get => a[n - 1 - i];
			set => a[n - 1 - i] = value;
		}
		public T First
		{
			get => a[n - 1];
			set => a[n - 1] = value;
		}

		public void Clear() => n = 0;
		public void Push(T item) => a[n++] = item;
		public T Pop() => a[--n];
	}
}
