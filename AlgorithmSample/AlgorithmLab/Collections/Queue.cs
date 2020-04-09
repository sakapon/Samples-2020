using System;

namespace AlgorithmLab.Collections
{
	public class Queue<T>
	{
		T[] a;
		int index;
		public T[] Raw => a;
		public int Length { get; private set; }
		public T First
		{
			get
			{
				if (Length == 0) throw new InvalidOperationException();
				return a[index];
			}
		}

		public Queue(int size)
		{
			if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
			a = new T[size];
		}

		public void Push(T item)
		{
			if (a.Length <= Length) throw new InvalidOperationException();
			a[(index + Length++) % a.Length] = item;
		}

		public T Pop()
		{
			if (Length == 0) throw new InvalidOperationException();
			var r = a[index];
			Length--;
			index = (index + 1) % a.Length;
			return r;
		}

		public T[] ToArray()
		{
			var r = new T[Length];
			for (int i = 0; i < Length; i++)
				r[i] = a[(index + i) % a.Length];
			return r;
		}
	}
}
