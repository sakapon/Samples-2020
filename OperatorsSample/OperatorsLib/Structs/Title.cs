﻿using System;

namespace OperatorsLib.Structs
{
	public struct Title : IEquatable<Title>, IComparable<Title>
	{
		public string Name { get; }
		public int Number { get; }

		public Title(string name, int number) => (Name, Number) = (name, number);
		public override string ToString() => $"{Name} #{Number}";

		#region Equality Operators

		public bool Equals(Title other) => Name == other.Name && Number == other.Number;

		public static bool operator ==(Title v1, Title v2) => v1.Equals(v2);
		public static bool operator !=(Title v1, Title v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is Title v && Equals(v);
		public override int GetHashCode() => HashCode.Combine(Name, Number);

		// HashCode.Combine を利用できない場合
		//public override int GetHashCode() => (Name, Number).GetHashCode();
		#endregion

		#region Comparison Operators

		public int CompareTo(Title other)
		{
			// 参照型の場合は null 値があるため、静的メソッドが実装されることも多いです。
			var c1 = string.Compare(Name, other.Name);
			if (c1 != 0) return c1;
			return Number.CompareTo(other.Number);
		}

		public static bool operator <(Title v1, Title v2) => v1.CompareTo(v2) < 0;
		public static bool operator >(Title v1, Title v2) => v1.CompareTo(v2) > 0;
		public static bool operator <=(Title v1, Title v2) => v1.CompareTo(v2) <= 0;
		public static bool operator >=(Title v1, Title v2) => v1.CompareTo(v2) >= 0;
		#endregion
	}
}
