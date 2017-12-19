using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
	public static class Extensions
	{
		public static bool Compare<T>(this string op, T lhs, T rhs) where T : IComparable<T>
		{
			switch (op)
			{
				case "<": return lhs.CompareTo(rhs) < 0;
				case ">": return lhs.CompareTo(rhs) > 0;
				case "<=": return lhs.CompareTo(rhs) <= 0;
				case ">=": return lhs.CompareTo(rhs) >= 0;
				case "==": return lhs.Equals(rhs);
				case "!=": return !lhs.Equals(rhs);
				default: throw new ArgumentException("Invalid comparison operator: {0}", op);
			}
		}

		public static int Operator(this string op, int lhs, int rhs)
		{
			switch (op)
			{
				case "inc": return lhs + rhs;
				case "dec": return lhs - rhs;
				default: throw new ArgumentException("Invalid comparison operator: {0}", op);
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var input = File.ReadAllLines("../../input.txt");

			var instructions = new List<string[]>();
			foreach (var instruction in input)
			{
				instructions.Add(instruction.Split(' '));
			}

			int highest = 0;
			var registers = new Dictionary<string, int>();
			foreach (var instruction in instructions)
			{
				if (!registers.ContainsKey(instruction[0]))
					registers[instruction[0]] = 0;
				if (!registers.ContainsKey(instruction[4]))
					registers[instruction[4]] = 0;

				if (instruction[5].Compare(registers[instruction[4]], int.Parse(instruction[6])))
				{
					registers[instruction[0]] = instruction[1].Operator(registers[instruction[0]], int.Parse(instruction[2]));
					if (registers[instruction[0]] > highest)
					{
						highest = registers[instruction[0]];
					}
				}
			}

			Console.WriteLine($"Part One: {registers.Values.Max()}");
			Console.WriteLine($"Part Two: {highest}");

			Console.Read();
		}
	}
}
