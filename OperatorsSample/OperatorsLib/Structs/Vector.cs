using System;

namespace OperatorsLib.Structs
{
	// デバッグ時の表示を ToString と異なるものにしたい場合、[DebuggerDisplay] を追加します。
	public struct Vector : IEquatable<Vector>
	{
		public static Vector Zero { get; } = new Vector();
		public static Vector UnitX { get; } = new Vector(1, 0);
		public static Vector UnitY { get; } = new Vector(0, 1);

		public double X { get; }
		public double Y { get; }
		public double Norm => Math.Sqrt(X * X + Y * Y);
		public double Angle => Math.Atan2(Y, X);

		public Vector(double x, double y) => (X, Y) = (x, y);
		public override string ToString() => $"({X}, {Y})";

		#region Equality Operators

		public bool Equals(Vector other) => X == other.X && Y == other.Y;

		public static bool operator ==(Vector v1, Vector v2) => v1.Equals(v2);
		public static bool operator !=(Vector v1, Vector v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is Vector v && Equals(v);
		public override int GetHashCode() => HashCode.Combine(X, Y);
		#endregion

		#region Unary Operators
		public static Vector operator +(Vector v) => v;
		public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y);
		#endregion

		#region Binary Operators
		public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
		public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);
		public static Vector operator *(double c, Vector v) => new Vector(v.X * c, v.Y * c);
		public static Vector operator *(Vector v, double c) => new Vector(v.X * c, v.Y * c);
		public static Vector operator /(Vector v, double c) => new Vector(v.X / c, v.Y / c);

		// ドット積 (dot product)、内積 (inner product)
		// 実際には静的メソッドとして定義することが多いです。
		public static double operator *(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y;
		#endregion

		public static double Area(Vector v1, Vector v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y) / 2;
	}
}
