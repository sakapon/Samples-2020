using System;
using System.Diagnostics;

namespace OperatorsLib.Structs
{
	// デバッグ時の表示を ToString と異なるものにしたい場合、[DebuggerDisplay] を追加します。
	[DebuggerDisplay(@"\{{Value.ToString(""X8"")}\}")]
	public struct BitArray
	{
		public int Value { get; private set; }

		public bool this[int index]
		{
			get => (Value & (1 << index)) != 0;
			set
			{
				if (value) Value |= 1 << index;
				else Value &= ~(1 << index);
			}
		}

		public bool this[Index index]
		{
			get => this[index.GetOffset(32)];
			set => this[index.GetOffset(32)] = value;
		}

		public BitArray(int value) => Value = value;
		public override string ToString() => Value.ToString();
		public static BitArray Parse(string s) => int.Parse(s);

		public static implicit operator BitArray(int v) => new BitArray(v);
		public static explicit operator int(BitArray v) => v.Value;

		public static BitArray operator ++(BitArray v) => v.Value + 1;
		public static BitArray operator --(BitArray v) => v.Value - 1;
	}
}
