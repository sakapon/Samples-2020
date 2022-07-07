// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_B
// 容量を固定します。
namespace AlgorithmLab.Collections.Arrays.ArrayQueue101
{
	public class ArrayQueue<T>
	{
		T[] a;
		int fi, li;

		public ArrayQueue(int capacity) => a = new T[capacity];

		public int Count => li - fi;
		public T this[int i]
		{
			get => a[fi + i];
			set => a[fi + i] = value;
		}
		public T First
		{
			get => a[fi];
			set => a[fi] = value;
		}

		public void Clear() => fi = li = 0;
		public void Add(T item) => a[li++] = item;
		public T Pop() => a[fi++];
	}
}
