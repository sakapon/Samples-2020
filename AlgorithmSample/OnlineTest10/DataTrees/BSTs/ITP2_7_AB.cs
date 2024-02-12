using AlgorithmLib10.DataTrees.BSTs.Trees102;

namespace OnlineTest.DataTrees.BSTs
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_B
	class ITP2_7_AB
	{
		static void Main()
		{
			var qc = int.Parse(Console.ReadLine());

			var set = new Int32TreeSet();

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			while (qc-- > 0)
			{
				var q = Console.ReadLine().Split();
				var x = int.Parse(q[1]);

				if (q[0][0] == '0')
				{
					set.Add(x);
					Console.WriteLine(set.Count);
				}
				else if (q[0][0] == '1')
				{
					Console.WriteLine(set.Contains(x) ? 1 : 0);
				}
				else
				{
					set.Remove(x);
				}
			}
			Console.Out.Flush();
		}
	}
}
