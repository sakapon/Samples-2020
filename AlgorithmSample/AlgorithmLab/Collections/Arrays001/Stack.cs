using System;

namespace AlgorithmLab.Collections.Arrays001
{
	public class Stack<T>
	{
		T[] a;
		public T[] Raw => a;
		public int Length { get; private set; }
		public T First
		{
			get
			{
				if (Length == 0) throw new InvalidOperationException();
				return a[Length - 1];
			}
		}

		public Stack(int size)
		{
			if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
			a = new T[size];
		}

		public void Push(T item)
		{
			if (a.Length <= Length) throw new InvalidOperationException();
			a[Length++] = item;
		}

		public T Pop()
		{
			if (Length == 0) throw new InvalidOperationException();
			return a[--Length];
		}

		public T[] ToArray()
		{
			var r = new T[Length];
			Array.Copy(a, r, Length);
			Array.Reverse(r);
			return r;
		}
	}
}
