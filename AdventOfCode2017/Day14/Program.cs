using Day10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
	class Program
	{
		static void Main(string[] args)
		{
			var key = "ffayrhll";

			var grid = new string[128];
			for (int i = 0; i < 128; ++i)
			{
				var hex = $"{key}-{i}".ToKnotHash64();
				grid[i] = string.Join("", hex.Select(c => Convert.ToString(int.Parse(
					c.ToString(), System.Globalization.NumberStyles.HexNumber), 2).PadLeft(4, '0')));
			}

			int groups = 0;
			var closed = new HashSet<int[]>();
			var neighbours = new[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
			for (int i = 0; i < 128; ++i)
			{
				for (int j = 0; j < 128; ++j)
				{
					if (grid[i][j] == '0') continue;
					if (closed.Any(e => e[0] == i && e[1] == j)) continue;

					var open = new Queue<int[]>();
					open.Enqueue(new[] { i, j });
					groups++;

					while (open.Count > 0)
					{
						var visited = open.Dequeue();
						for (int k = 0; k < 4; ++k)
						{
							var x = visited[0] + neighbours[k, 0];
							var y = visited[1] + neighbours[k, 1];
							if (x < 0 || x > 127 || y < 0 || y > 127)
							{
								continue;
							}
							if (grid[x][y] == '1' && !closed.Any(e => e[0] == x && e[1] == y))
							{
								open.Enqueue(new[] { x, y });
								closed.Add(new int[] { x, y });
							}
						}
					}
				}
			}

			Console.WriteLine($"Part One: {grid.Sum(s => s.Count(c => c == '1'))}");
			Console.WriteLine($"Part Two: {groups}");

			Console.Read();
		}
	}
}
