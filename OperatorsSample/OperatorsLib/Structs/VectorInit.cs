using System;

namespace OperatorsLib.Structs
{
	public struct VectorInit
	{
		public double X { get; }
		public double Y { get; }

		public double Norm { get; }
		public double Angle { get; }

		// ctor() と ctor(0, 0) の結果が同等になる必要があります。
		public VectorInit(double x, double y)
		{
			(X, Y) = (x, y);
			Norm = Math.Sqrt(X * X + Y * Y);
			Angle = Math.Atan2(Y, X);
		}
	}
}
