using System;

namespace OperatorsLib.Classes
{
	// デバッグ時の表示を ToString と異なるものにしたい場合、[DebuggerDisplay] を追加します。
	public class Title : IEquatable<Title>, IComparable<Title>
	{
		public string Name { get; }
		public int Number { get; }

		public Title(string name, int number) => (Name, Number) = (name, number);
		public override string ToString() => $"{Name} #{Number}";

		#region Equality Operators

		public bool Equals(Title other) => other != null && Name == other.Name && Number == other.Number;

		// 参照型の場合は null 値があるため、静的メソッドが実装されることも多いです。
		public static bool Equals(Title v1, Title v2) => v1?.Equals(v2) ?? (v2 == null);

		public static bool operator ==(Title v1, Title v2) => Equals(v1, v2);
		public static bool operator !=(Title v1, Title v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => obj is Title v && Equals(v);
		public override int GetHashCode() => HashCode.Combine(Name, Number);
		#endregion

		#region Comparison Operators

		public int CompareTo(Title other)
		{
			if (other == null) return 1;

			var c1 = string.Compare(Name, other.Name);
			if (c1 != 0) return c1;
			return Number.CompareTo(other.Number);
		}

		// 参照型の場合は null 値があるため、静的メソッドが実装されることも多いです。
		public static int Compare(Title v1, Title v2) => v1?.CompareTo(v2) ?? (v2 == null ? 0 : -1);

		public static bool operator <(Title v1, Title v2) => Compare(v1, v2) < 0;
		public static bool operator >(Title v1, Title v2) => Compare(v1, v2) > 0;
		public static bool operator <=(Title v1, Title v2) => Compare(v1, v2) <= 0;
		public static bool operator >=(Title v1, Title v2) => Compare(v1, v2) >= 0;
		#endregion
	}
}
