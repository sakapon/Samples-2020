namespace OperatorsLib.Structs
{
	public struct Vector2
	{
		public double X { get; }
		public double Y { get; }

		public Vector2(double x, double y) => (X, Y) = (x, y);
		public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);

		public static implicit operator Vector2((double x, double y) v) => new Vector2(v.x, v.y);
		public static explicit operator (double x, double y)(Vector2 v) => (v.X, v.Y);
	}
}
