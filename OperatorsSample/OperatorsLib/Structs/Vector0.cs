namespace OperatorsLib.Structs
{
	public struct Vector0
	{
		public double X { get; }
		public double Y { get; }

		public Vector0(double x, double y) => (X, Y) = (x, y);
	}
}
