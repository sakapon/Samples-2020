namespace OperatorsLib.Structs
{
	public struct StringBool
	{
		public static StringBool True { get; } = bool.TrueString;
		public static StringBool False { get; } = bool.FalseString;
		public static StringBool Unknown { get; } = null;

		// bool? として持つこともできますが、この例ではあえて論理演算を自作します。
		// Ternary 構造体では bool? としています。
		public string Value { get; }

		public bool IsTrue => bool.TryParse(Value, out var b) && b;
		public bool IsFalse => bool.TryParse(Value, out var b) && !b;
		public bool IsUnknown => !bool.TryParse(Value, out var _);

		public StringBool(string value) => Value = value;
		public override string ToString() => Value ?? "Unknown";

		public static implicit operator StringBool(string v) => new StringBool(v);
		public static explicit operator bool?(StringBool v) => v.IsUnknown ? default(bool?) : v.IsTrue;

		public static bool operator true(StringBool v) => v.IsTrue;
		public static bool operator false(StringBool v) => v.IsFalse;

		public static StringBool operator !(StringBool v) => v.IsUnknown ? Unknown : v.IsTrue ? False : True;
		public static StringBool operator &(StringBool v1, StringBool v2) => v1.IsFalse || v2.IsFalse ? False : v1.IsUnknown || v2.IsUnknown ? Unknown : True;
		public static StringBool operator ^(StringBool v1, StringBool v2) => v1.IsUnknown || v2.IsUnknown ? Unknown : v1.IsTrue ^ v2.IsTrue ? True : False;
		public static StringBool operator |(StringBool v1, StringBool v2) => v1.IsTrue || v2.IsTrue ? True : v1.IsUnknown || v2.IsUnknown ? Unknown : False;
	}
}
