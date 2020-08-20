using System;
using System.Reflection;

namespace OperatorsLib.Classes
{
	public class EquatableObject
	{
		// ValueType と同様に、すべてのフィールドで等値性を評価します。
		public override bool Equals(object obj)
		{
			var type = GetType();
			if (type != obj?.GetType()) return false;

			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				if (!Equals(field.GetValue(this), field.GetValue(obj))) return false;
			return true;
		}

		public override int GetHashCode()
		{
			var hc = default(HashCode);
			foreach (var field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				hc.Add(field.GetValue(this));
			return hc.ToHashCode();
		}
	}

	public class VectorEq : EquatableObject
	{
		public double X { get; }
		public double Y { get; }

		// 必要なときに計算してキャッシュしておく例。
		// しかし、すべてのフィールドで等値性が評価されるため、この実装方法では途中でその評価結果が変わる可能性があります。
		double norm = double.NaN;
		public double Norm => double.IsNaN(norm) ? (norm = Math.Sqrt(X * X + Y * Y)) : norm;
		double angle = double.NaN;
		public double Angle => double.IsNaN(angle) ? (angle = Math.Atan2(Y, X)) : angle;

		public VectorEq(double x, double y) => (X, Y) = (x, y);
	}
}
