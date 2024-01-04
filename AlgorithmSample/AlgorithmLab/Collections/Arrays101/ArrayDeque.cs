// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
// 容量を固定します。
namespace AlgorithmLab.Collections.Arrays101
{
	public class ArrayDeque<T>
	{
		T[] a;
		int fi, li;

		public ArrayDeque(int capacity) => a = new T[(fi = li = capacity) << 1];

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
		public T Last
		{
			get => a[li - 1];
			set => a[li - 1] = value;
		}

		public void Clear() => fi = li = a.Length >> 1;
		public void AddFirst(T item) => a[--fi] = item;
		public void AddLast(T item) => a[li++] = item;
		public T PopFirst() => a[fi++];
		public T PopLast() => a[--li];
	}
}
