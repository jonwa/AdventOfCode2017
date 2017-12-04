using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
	class Program
	{
		interface IPassphrase
		{
			bool Test();
			bool IsValid(string str);
		}

		class PartOne : IPassphrase
		{
			public bool Test()
			{
				var tests = new[] { "aa bb cc dd ee", "aa bb cc dd aa", "aa bb cc dd aaa" };
				var solutions = new[] { true, false, true };
				for (int i = 0; i < tests.Length; ++i)
				{
					if (IsValid(tests[i]) != solutions[i])
					{
						return false;
					}
				}
				return true;
			}

			public bool IsValid(string str)
			{
				string diff = string.Join(" ", str.Split(' ').Distinct());
				return diff.Length == str.Length;
			}
		}

		class PartTwo : IPassphrase
		{
			public bool Test()
			{
				var tests = new[] { "abcde fghij", "abcde xyz ecdab", "a ab abc abd abf abj", "iiii oiii ooii oooi oooo", "oiii ioii iioi iiio" };
				var solutions = new[] { true, false, true, true, false };
				for (int i = 0; i < tests.Length; ++i)
				{
					if (IsValid(tests[i]) != solutions[i])
					{
						return false;
					}
				}
				return true;
			}

			public bool IsValid(string str)
			{
				string diff = string.Join(" ", str.Split(' ').Select(s => s = String.Concat(s.OrderBy(c => c))).Distinct());
				return diff.Length == str.Length;
			}
		}

		static int Run<T>(string[] input) where T : IPassphrase, new()
		{
			var o = new T();
			var t = o.Test();
			Console.WriteLine(t ? $"{o.GetType().Name}: Succeeded" : $"{o.GetType().Name} Failed");
			return input.ToList().Sum(s => o.IsValid(s) ? 1 : 0);
		}

		static void Main(string[] args)
		{
			var input = File.ReadAllLines("../../input.txt");
			Console.WriteLine($"Solution: {Run<PartOne>(input)}");
			Console.WriteLine();
			Console.WriteLine($"Solution: {Run<PartTwo>(input)}");
			Console.Read();
		}
	}
}
