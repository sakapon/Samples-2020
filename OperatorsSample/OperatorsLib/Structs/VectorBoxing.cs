using System;

namespace OperatorsLib.Structs
{
	public struct VectorBoxing : IEquatable<VectorBoxing>
	{
		public double X { get; }
		public double Y { get; }

		public VectorBoxing(double x, double y) => (X, Y) = (x, y);

		#region Equality Operators
		public bool Equals(VectorBoxing other) => base.Equals(other);
		public static bool operator ==(VectorBoxing v1, VectorBoxing v2) => v1.Equals(v2);
		public static bool operator !=(VectorBoxing v1, VectorBoxing v2) => !v1.Equals(v2);
		// 下の 2 つは原理上は不要ですが、警告が出るためオーバーライドします。
		public override bool Equals(object obj) => base.Equals(obj);
		public override int GetHashCode() => base.GetHashCode();
		#endregion
	}
}
