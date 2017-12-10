using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
	public enum Direction
	{
		North, West, South, East
	}

	public class Node
	{
		public int x { get; set; }
		public int y { get; set; }
		public int value { get; set; }
		public int adjValue { get; set; }
	}

	public interface IGrid
	{
		Node[] nodes { get; set; }
		int GetManhattanDistance(Node a, Node b);
		int GetAdjacentSum();
	}

	public class Grid : IGrid
	{
		public Node[] nodes { get; set; }

		public Grid(int size)
		{
			nodes = new Node[size];
			nodes[0] = new Node { x = 0, y = 0, value = 1, adjValue = 1 };

			if (size == 1)
			{
				return;
			}

			int x = 0, y = 0;
			int step = 1, steps = 2;
			var direction = Direction.East;

			for (int i = 1; i < size; ++i)
			{
				switch (direction)
				{
					case Direction.North: y++; break;
					case Direction.South: y--; break;
					case Direction.East: x++; break;
					case Direction.West: x--; break;
				}

				step++;
				nodes[i] = new Node { x = x, y = y, value = i + 1 };

				if (step < steps)
				{
					continue;
				}

				step = 0;
				direction = NextDirection(direction);
				if (direction == Direction.North)
				{
					step++;
					if (i == 1)
					{
						continue;
					}

					x++;
					steps += 2;
					nodes[++i] = new Node { x = x, y = y, value = i + 1 };
				}
			}
		}

		public int GetAdjacentSum()
		{
			Node current, neighbour;
			var directions = new[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };
			int nx = 0, ny = 0, len = nodes.Length;

			for(int i = 0; i < len; ++i)
			{
				current = nodes[i];
				for (int j = 0; j < directions.Length / 2; ++j)
				{
					nx = current.x + directions[j, 0];
					ny = current.y + directions[j, 1];
					neighbour = nodes.FirstOrDefault(n => n.x == nx && n.y == ny);
					if (neighbour == null || neighbour.value >= current.value)
					{
						continue;
					}

					current.adjValue += neighbour.adjValue;
					if (current.adjValue > len)
					{
						return current.adjValue;
					}
				}
			}
			
			return -1;
		}

		public int GetManhattanDistance(Node a, Node b)
		{
			return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
		}

		private Direction NextDirection(Direction current)
		{
			switch (current)
			{
				case Direction.North:
					return Direction.West;
				case Direction.East:
					return Direction.North;
				case Direction.South:
					return Direction.East;
				case Direction.West:
					return Direction.South;
				default:
					return Direction.North;
			}
		}
	}
}
