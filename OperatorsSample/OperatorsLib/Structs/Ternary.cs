namespace OperatorsLib.Structs
{
	public struct Ternary
	{
		public bool? Value { get; }

		public Ternary(bool? value) => Value = value;
		public override string ToString() => Value?.ToString() ?? "Null";

		public static implicit operator Ternary(bool? v) => new Ternary(v);
		public static explicit operator bool?(Ternary v) => v.Value;

		// true 演算子の代わりに bool 型への暗黙的型変換を宣言することでも、if ステートメントなどの制御条件式として使えます。
		// 両方を宣言した場合は、この暗黙的型変換が優先されるようです。
		//public static implicit operator bool(Ternary v) => v.Value == true;

		public static bool operator true(Ternary v) => v.Value == true;
		public static bool operator false(Ternary v) => v.Value == false;

		public static Ternary operator !(Ternary v) => !v.Value;
		public static Ternary operator &(Ternary v1, Ternary v2) => v1.Value & v2.Value;
		public static Ternary operator ^(Ternary v1, Ternary v2) => v1.Value ^ v2.Value;
		public static Ternary operator |(Ternary v1, Ternary v2) => v1.Value | v2.Value;
	}
}
