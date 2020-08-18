using System;

namespace OperatorsLib.Classes
{
	public class Title : IEquatable<Title>, IComparable<Title>
	{
		public string Name { get; }
		public int Number { get; }

		public Title(string name, int number) => (Name, Number) = (name, number);
		public override string ToString() => $"{Name} #{Number}";

		#region Equality Operators

		// other != null では無限ループ。
		public bool Equals(Title other) => !(other is null) && Name == other.Name && Number == other.Number;

		// 参照型の場合は null 値があるため、静的メソッドが実装されることも多いです。
		public static bool Equals(Title v1, Title v2) => v1?.Equals(v2) ?? (v2 is null);

		public static bool operator ==(Title v1, Title v2) => Equals(v1, v2);
		public static bool operator !=(Title v1, Title v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as Title);
		public override int GetHashCode() => HashCode.Combine(Name, Number);

		// HashCode.Combine を利用できない場合
		//public override int GetHashCode() => (Name, Number).GetHashCode();
		#endregion

		#region Comparison Operators

		public int CompareTo(Title other)
		{
			if (other is null) return 1;

			var c1 = string.Compare(Name, other.Name);
			if (c1 != 0) return c1;
			return Number.CompareTo(other.Number);
		}

		// 参照型の場合は null 値があるため、静的メソッドが実装されることも多いです。
		public static int Compare(Title v1, Title v2) => v1?.CompareTo(v2) ?? (v2 is null ? 0 : -1);

		public static bool operator <(Title v1, Title v2) => Compare(v1, v2) < 0;
		public static bool operator >(Title v1, Title v2) => Compare(v1, v2) > 0;
		public static bool operator <=(Title v1, Title v2) => Compare(v1, v2) <= 0;
		public static bool operator >=(Title v1, Title v2) => Compare(v1, v2) >= 0;
		#endregion
	}
}
