using System;
using System.IO;

namespace Day19
{
	class Program
	{
		static void Main(string[] args)
		{
			var diagram = File.ReadAllLines("../../input.txt");
			var position = Point.Make(diagram[0].IndexOf('|'), 0);
			var lastPosition = position;
			var direction = Point.Make(0, 1);
			var letters = string.Empty;
			int steps = 0;

			Func<int, int, Point> NextPosition = (int dx, int dy) =>
			{
				steps++;
				lastPosition = position;
				return Point.Make(position.x + dx, position.y + dy);
			};

			Func<int, int, Point> NextDirection = (int x, int y) =>
			{
				if (y + 1 < diagram.Length && diagram[y + 1][x] != ' ' && (lastPosition.y != y + 1 && lastPosition.x != x))
					return Point.Make(0, 1);
				if (y > 0 && diagram[y - 1][x] != ' ' && (lastPosition.y != y - 1 && lastPosition.x != x))
					return Point.Make(0, -1);
				if (x + 1 < diagram[0].Length && diagram[y][x + 1] != ' ' && (lastPosition.y != y && lastPosition.x != x + 1))
					return Point.Make(1, 0);
				if (x > 0 && diagram[y][x - 1] != ' ' && (lastPosition.y != y && lastPosition.x != x - 1))
					return Point.Make(-1, 0);
				return null;
			};

			bool running = true;
			while (running)
			{
				switch (diagram[position.y][position.x])
				{
					case ' ':
						break;
					case '|': 
					case '-':
						position = NextPosition(direction.x, direction.y);
						continue;
					case '+':
						direction = NextDirection(position.x, position.y);
						position = NextPosition(direction.x, direction.y);
						continue;
					default:
						letters = letters + diagram[position.y][position.x];
						position = NextPosition(direction.x, direction.y);
						if (diagram[position.y][position.x] == ' ')
						{
							running = false;
						}
						continue;
				}
			}

			Console.WriteLine($"Part One: {letters}");
			Console.WriteLine($"Part Two: {steps}");

			Console.Read();
		}

		class Point
		{
			public int x { get; set; }
			public int y { get; set; }

			private Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			public static Point Make(int x, int y)
			{
				return new Point(x, y);
			}
		}
	}
}
