using System;
using System.Linq;
using System.Text;
using AlgorithmLab.DataTrees.UF602;

namespace OnlineTest.DataTrees.UF
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bp
	class Typical90_068_2
	{
		struct Op
		{
			public int sign;
			public long value;

			public Op(int sign, long value)
			{
				this.sign = sign;
				this.value = value;
			}

			public long GetValue(long a) => sign * a + value;
			public Op Inverse() => new Op(sign, -sign * value);
			public static Op Composition(Op f, Op g) => new Op(f.sign * g.sign, f.sign * g.value + f.value);
		}

		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read4());
			var sb = new StringBuilder();

			var uf = new UnionFind<Op>(n + 1, new Op(1, 0), f => f.Inverse(), Op.Composition);

			foreach (var (t, x, y, v) in qs)
			{
				if (t == 0)
				{
					uf.Union(x, y, new Op(-1, v));
				}
				else
				{
					if (uf.AreSame(x, y))
					{
						var f = uf.GetX2Y(x, y);
						sb.AppendLine(f.GetValue(v).ToString());
					}
					else
					{
						sb.AppendLine("Ambiguous");
					}
				}
			}
			Console.Write(sb);
		}
	}
}
