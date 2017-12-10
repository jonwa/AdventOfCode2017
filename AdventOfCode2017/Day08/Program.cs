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

	public class Instruction
	{
		public string[] encoded { get; set; }

		public Instruction(string decoded)
		{
			encoded = decoded.Split(' ');
		}
	}

	public class Program
	{
		static void Run(string[] input)
		{
			var instructions = new List<Instruction>();
			foreach (var i in input)
			{
				var instruction = new Instruction(i);
				instructions.Add(instruction);
			}

			int highest = 0;
			var registers = new Dictionary<string, int>();
			foreach (var instruction in instructions)
			{
				if (!registers.ContainsKey(instruction.encoded[0]))
					registers[instruction.encoded[0]] = 0;
				if (!registers.ContainsKey(instruction.encoded[4]))
					registers[instruction.encoded[4]] = 0;

				if (instruction.encoded[5].Compare(registers[instruction.encoded[4]], int.Parse(instruction.encoded[6])))
				{
					registers[instruction.encoded[0]] = instruction.encoded[1].Operator(registers[instruction.encoded[0]], int.Parse(instruction.encoded[2]));
					if (registers[instruction.encoded[0]] > highest)
					{
						highest = registers[instruction.encoded[0]];
					}
				}
			}

			var maxValue = registers.Values.Max();


			Console.WriteLine("-------------------");
			Console.WriteLine($"Part One: {maxValue}");
			Console.WriteLine($"Part Two: {highest}");
			Console.WriteLine("-------------------");
			Console.WriteLine();
		}

		static void Main(string[] args)
		{
			var test = File.ReadAllLines("../../test.txt");
			var input = File.ReadAllLines("../../input.txt");

			Console.WriteLine("Test");
			Run(test);
			Console.WriteLine("Solution");
			Run(input);
			Console.Read();
		}
	}
}
