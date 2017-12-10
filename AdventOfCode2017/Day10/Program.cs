using System;

namespace Day10
{
	public interface ISolution
	{
		void Test();
		void Solve(string input);
	}

	public class PartOne : ISolution
	{
		public void Solve(string input)
		{
			var result = input.ToKnotHash();
			Console.WriteLine($"Part One: {result[0] * result[1]}");
		}

		public void Test() { }
	}

	public class PartTwo : ISolution
	{
		public void Solve(string input)
		{
			var result = input.ToKnotHash64();
			Console.WriteLine($"Part Two: {result}");
		}

		public void Test()
		{
			var inputs = new[] { "", "AoC 2017", "1,2,3", "1,2,4" };
			var answers = new[] { "a2582a3a0e66e6e86e3812dcb672a272", "33efeb34ea91902bb2f59c9920caa6cd", "3efbe78a8d82f29979031a4aa0b16a9d", "63960835bcdc130f0b66d7ff4f6a5a8e" };
			for (int i = 0; i < inputs.Length; ++i)
			{
				if (inputs[i].ToKnotHash64() != answers[i])
				{
					throw new Exception();
				}
			}
		}
	}

	class Program
	{
		static void Run<T>(string input) where T : ISolution, new()
		{
			var t = new T();
			t.Test();
			t.Solve(input);
		}

		static void Main(string[] args)
		{
			var input = "63,144,180,149,1,255,167,84,125,65,188,0,2,254,229,24";
			Run<PartOne>(input);
			Run<PartTwo>(input);
			Console.Read();
		}
	}
}
