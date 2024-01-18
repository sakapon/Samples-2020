using System;

namespace AlgorithmLab.Collections.Arrays001
{
	public class List<T>
	{
		T[] a;
		public T[] Raw => a;
		public int Length { get; private set; }

		public T this[int index]
		{
			get
			{
				if (index < 0 || Length <= index) throw new ArgumentOutOfRangeException(nameof(index));
				return a[index];
			}
			set
			{
				if (index < 0 || Length <= index) throw new ArgumentOutOfRangeException(nameof(index));
				a[index] = value;
			}
		}

		public List(int size)
		{
			if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
			a = new T[size];
		}

		public List(T[] array, int length)
		{
			a = array ?? throw new ArgumentNullException(nameof(array));
			if (length < 0 || a.Length < length) throw new ArgumentOutOfRangeException(nameof(length));
			Length = length;
		}

		public void Add(T item)
		{
			if (a.Length <= Length) throw new InvalidOperationException();
			a[Length++] = item;
		}

		public void Insert(int index, T item)
		{
			if (index < 0 || Length < index) throw new ArgumentOutOfRangeException(nameof(index));
			if (a.Length <= Length) throw new InvalidOperationException();
			for (int i = Length - 1; i >= index; i--)
				a[i + 1] = a[i];
			a[index] = item;
			Length++;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || Length <= index) throw new ArgumentOutOfRangeException(nameof(index));
			Length--;
			for (int i = index; i < Length; i++)
				a[i] = a[i + 1];
		}

		public void Clear()
		{
			Length = 0;
		}

		public T[] ToArray()
		{
			var r = new T[Length];
			Array.Copy(a, r, Length);
			return r;
		}
	}
}
