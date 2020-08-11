using System;

namespace OperatorsLib.Structs
{
	// デバッグ時の表示を ToString と異なるものにしたい場合、[DebuggerDisplay] を追加します。
	public struct Title : IComparable<Title>
	{
		public string Name { get; }
		public int Number { get; }

		public Title(string name, int number) => (Name, Number) = (name, number);
		public override string ToString() => $"{Name} #{Number}";

		#region Comparison Operators

		public int CompareTo(Title other)
		{
			// 参照型の場合は null 値があるため、静的メソッドで実装されることが多いです。
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
