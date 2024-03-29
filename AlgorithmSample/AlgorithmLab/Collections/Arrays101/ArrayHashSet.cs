﻿using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays101
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashSet<T>
	{
		int n;
		readonly int f;
		readonly int[] firsts;
		readonly int[] nexts;
		readonly bool[] u;
		readonly T[] a;
		readonly IEqualityComparer<T> ec = typeof(T) == typeof(string) ? (IEqualityComparer<T>)StringComparer.Ordinal : EqualityComparer<T>.Default;

		public ArrayHashSet(int capacity)
		{
			f = capacity - 1;
			firsts = new int[capacity];
			nexts = new int[capacity];
			u = new bool[capacity];
			a = new T[capacity];
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
		}

		public int Count => n;

		public void Clear()
		{
			n = 0;
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
			Array.Clear(u, 0, u.Length);
		}

		public bool Contains(T item)
		{
			var h = item.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(a[i], item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			var h = item.GetHashCode() & f;
			var last = -1;
			var i = firsts[h];
			for (; i != -1; last = i, i = nexts[i])
				if (ec.Equals(a[i], item)) return false;

			i = last == -1 ? h : (last + 1) & f;
			while (u[i]) i = (i + 1) & f;

			++n;
			if (last == -1) firsts[h] = i;
			else nexts[last] = i;
			u[i] = true;
			a[i] = item;
			return true;
		}

		public bool Remove(T item)
		{
			var h = item.GetHashCode() & f;
			var last = -1;
			for (var i = firsts[h]; i != -1; last = i, i = nexts[i])
			{
				if (!ec.Equals(a[i], item)) continue;

				--n;
				if (last == -1) firsts[h] = nexts[i];
				else nexts[last] = nexts[i];
				nexts[i] = -1;
				u[i] = false;
				return true;
			}
			return false;
		}

		IEnumerable<int> GetAddresses(int h)
		{
			for (var i = firsts[h]; i != -1; i = nexts[i])
				yield return i;
		}
	}
}
