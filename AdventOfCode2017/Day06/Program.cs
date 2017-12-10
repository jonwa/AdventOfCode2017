using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
	class Program
	{
		interface ITest
		{
			int[] A { get; }
			int Answer { get; }
		}

		public struct PartOne : ITest
		{
			public int[] A { get => new[] { 0, 2, 7, 0 }; }
			public int Answer { get => 5; }
		}

		public struct PartTwo : ITest
		{
			public int[] A { get => new[] { 2, 4, 1, 2 }; }
			public int Answer { get => 4; }
		}

		public static int Solve(int[] A)
		{
			var visited = new List<string>();
			visited.Add(string.Join("", A));

			do
			{
				int index = 0, maxValue = 0, maxIndex = -1;
				A.Select(value =>
				{
					if (maxIndex == -1 || value > maxValue)
					{
						maxIndex = index;
						maxValue = value;
					}
					index++;
					return maxIndex;
				}).Last();

				A[maxIndex] = 0;

				for (int i = maxIndex + 1; i <= maxValue + maxIndex; ++i)
				{
					A[i % A.Length]++;
				}

				var state = string.Join("", A);
				if (visited.Contains(state))
				{
					break;
				}
				visited.Add(state);
			} while (true);

			return visited.Count;
		}

		static int Solve<T>(int[] A) where T : ITest, new()
		{
			var test = new T();
			var result = Solve(test.A) == test.Answer;
			Console.WriteLine(result ? $"{test.GetType().Name} Test - Succeeded." : $"{test.GetType().Name} Test - Failed.");
			return Solve(A);
		}

		static void Main(string[] args)
		{
			int[] A = new[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };
			Console.WriteLine($"Solution: { Solve<PartOne>(A) }");
			Console.WriteLine($"Solution: { Solve<PartTwo>(A) }");

			Console.Read();
		}
	}
}
