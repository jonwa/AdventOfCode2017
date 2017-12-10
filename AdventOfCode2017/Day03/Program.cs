using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
	class Program
	{
		static int SolvePartOne(int size)
		{
			var grid = new Grid(size);
			var first = grid.nodes.First();
			var last = grid.nodes.Last();
			return grid.GetManhattanDistance(first, last);
		}

		static int SolvePartTwo(int size)
		{
			var grid = new Grid(size);
			return grid.GetAdjacentSum();
		}

		static void Main(string[] args)
		{
			var input = 312051;

			Console.WriteLine("[Part One]");
			Console.WriteLine($"Tests: {SolvePartOne(1)} {SolvePartOne(12)} {SolvePartOne(23)} {SolvePartOne(1024)}");
			Console.WriteLine($"Solution: {SolvePartOne(input)}");

			Console.WriteLine();
			Console.WriteLine("[Part Two]");
			Console.WriteLine($"Solution: {SolvePartTwo(input)}");

			Console.ReadLine();
		}
	}
}
