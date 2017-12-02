using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Day2
{
	class Program
	{
		class Runner<T> where T : IChecksum, new()
		{
			public bool Test(string input, int expected)
			{
				if (Solve(input) != expected)
				{
					throw new Exception();
				}
				return true;
			}

			public int Solve(string input)
			{
				input = input.Replace('\r', '\t');

				var checksum = new T();
				var buckets = input.Split('\n');
				var split = input.Split('\t');
				var A = Array.ConvertAll(split, s => int.Parse(s));
				var rows = buckets.Length;
				var cols = A.Length / rows;
				int sum = 0;

				for (int i = 0; i < rows; ++i)
				{
					var row = A.ToList().GetRange(i * cols, cols);
					sum += checksum.Calc(row.ToArray());
				}

				return sum;
			}
		}

		interface IChecksum
		{
			int Calc(int[] A);
		}

		struct PartOne : IChecksum
		{
			public int Calc(int[] A)
			{
				int min = A.Where(i => i > 0).Min();
				int max = A.Max();
				return (max - min);
			}
		}

		struct PartTwo : IChecksum
		{
			public int Calc(int[] A)
			{
				int min = 0, max = 0;
				for (int i = 0; i < A.Length; ++i)
				{
					min = A.Where(n => n != A[i]).SingleOrDefault(n => A[i] % n == 0);
					if (min != 0)
					{
						max = Math.Max(min, A[i]);
						break;
					}
				}

				return (int)(max / min);
			}
		}

		static void Main(string[] args)
		{
			var partOne = new Runner<PartOne>();
			partOne.Test("5\t1\t9\t5\n\t7\t5\t3\t0\t\n2\t4\t6\t8", 18);

			var partTwo = new Runner<PartTwo>();
			partTwo.Test("5\t9\t2\t8\n\t9\t4\t7\t3\t\n3\t8\t6\t5", 9);

			var input = ConfigurationManager.AppSettings["input"];
			var solution1 = partOne.Solve(input);
			var solution2 = partTwo.Solve(input);

			Console.WriteLine($"Solution Part One: {solution1}");
			Console.WriteLine($"Solution Part Two: {solution2}");

			Console.Read();
		}
	}
}
