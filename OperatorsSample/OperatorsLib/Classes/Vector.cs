using System;

namespace OperatorsLib.Classes
{
	// デバッグ時の表示を ToString と異なるものにしたい場合、[DebuggerDisplay] を追加します。
	public class Vector : IEquatable<Vector>
	{
		public static Vector Zero { get; } = new Vector(0, 0);
		public static Vector UnitX { get; } = new Vector(1, 0);
		public static Vector UnitY { get; } = new Vector(0, 1);

		public double X { get; }
		public double Y { get; }

		// 必要なときに計算してキャッシュしておく例。
		Lazy<double> norm;
		public double Norm => norm.Value;
		Lazy<double> angle;
		public double Angle => angle.Value;

		public Vector(double x, double y)
		{
			(X, Y) = (x, y);
			norm = new Lazy<double>(() => Math.Sqrt(X * X + Y * Y));
			angle = new Lazy<double>(() => Math.Atan2(Y, X));
		}
		public override string ToString() => $"({X}, {Y})";

		#region Equality Operators

		// other != null では無限ループ。
		public bool Equals(Vector other) => !(other is null) && X == other.X && Y == other.Y;
		public static bool Equals(Vector v1, Vector v2) => v1?.Equals(v2) ?? (v2 is null);

		public static bool operator ==(Vector v1, Vector v2) => Equals(v1, v2);
		public static bool operator !=(Vector v1, Vector v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => obj is Vector v && Equals(v);
		public override int GetHashCode() => HashCode.Combine(X, Y);
		#endregion

		// 実際には null 値を考慮しなければなりません。
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

		public static double operator *(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y;
		#endregion

		public static double Area(Vector v1, Vector v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y) / 2;
	}
}
