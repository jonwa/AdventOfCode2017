using System;
using System.IO;

namespace Day11
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = File.ReadAllText("../../input.txt").Split(',');

			int x = 0, y = 0, z = 0;
			int steps = 0, highest = 0;

			Action<int, int, int> move = (int nx, int ny, int nz) => 
			{
				x += nx;
				y += ny;
				z += nz;
			};

			foreach (var direction in input)
			{
				switch (direction)
				{
					case "n": move(0, 1, -1); break;
					case "s": move(0, -1, 1); break;
					case "ne": move(1, 0, -1); break;
					case "nw": move(-1, 1, 0); break;
					case "se": move(1, -1, 0); break;
					case "sw": move(-1, 0, 1); break;
				}

				steps = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
				if (steps > highest)
				{
					highest = steps;
				}
			}

			Console.WriteLine($"Part One: {steps}");
			Console.WriteLine($"Part Two: {highest}");
			Console.Read();
		}
	}
}
