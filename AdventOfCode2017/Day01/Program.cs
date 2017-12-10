using System;
using System.Configuration;
using System.Linq;

namespace Day1
{
	class Program
	{
		interface ICaptcha
		{
			bool Test();
			int Solve(string input, int steps);
		}

		abstract class Captcha : ICaptcha
		{
			public abstract bool Test();

			public virtual int Solve(string input, int steps)
			{
				int lhs = 0, rhs = 0, sum = 0;

				for (int i = 0; i < input.Length; ++i)
				{
					lhs = (int)Char.GetNumericValue(input[i]);
					rhs = (int)Char.GetNumericValue(input[(i + steps) % input.Length]);
					if (lhs == rhs)
					{
						sum += rhs;
					}
				}

				return sum;
			}
		}

		class PartOne : Captcha
		{
			public override bool Test()
			{
				var tests = new[] { "1122", "1111", "1234", "91212129" };
				var solutions = new[] { 3, 4, 0, 9 };

				for (int i = 0; i < tests.Length; ++i)
				{
					if (base.Solve(tests[i], 1) != solutions[i])
					{
						throw new Exception($"Failed on test {tests[i]}");
					}
				}

				return true;
			}
		}

		class PartTwo : Captcha
		{
			public override bool Test()
			{
				var tests = new[] { "1212", "1221", "123425", "123123", "12131415" };
				var solutions = new[] { 6, 0, 4, 12, 4 };
				
				for (int i = 0; i < tests.Length; ++i)
				{
					if (base.Solve(tests[i], tests[i].Length / 2) != solutions[i])
					{
						throw new Exception($"Failed on test {tests[i]}");
					}
				}

				return true;
			}
		}

		static bool Test<T>() where T : ICaptcha, new()
		{
			return new T().Test();
		}

		static int Solve<T>(string input, int steps) where T : ICaptcha, new()
		{
			return new T().Solve(input, steps);
		}

		static void Main(string[] args)
		{
			var input = ConfigurationManager.AppSettings["input"];

			Test<PartOne>();
			var partOne = Solve<PartOne>(input, 1);

			Test<PartTwo>();
			var partTwo = Solve<PartTwo>(input, input.Length / 2);

			Console.WriteLine($"Solution Part One: {partOne}");
			Console.WriteLine($"Solution Part Two: {partTwo}");

			Console.ReadLine();
		}
	}
}
