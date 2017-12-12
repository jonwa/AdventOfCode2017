using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
	public static class Extensions
	{
		public static string[] Split(this string str, string separator)
		{
			return str.Split(new[] { separator }, StringSplitOptions.None);
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var connections = new Dictionary<int, HashSet<int>>();
			foreach(var line in File.ReadAllLines("../../input.txt"))
			{
				var split = line.Split(" <-> ");
				connections[int.Parse(split[0])] =
					new HashSet<int>(split[1].Split(", ").Select(s => int.Parse(s)));
			}
			
			for(int i = 0; i < 2; ++i)
			{
				var closed = new HashSet<int>();
				int groups = 0, n = (i == 0 ? 1 : connections.Count);

				for(int j = 0; j < n; ++j)
				{
					if (closed.Contains(j))
					{
						continue;
					}

					var open = new Queue<int>(new[] { j });
					while (open.Count > 0)
					{
						var visiting = open.Dequeue();
						foreach (var neighbour in connections[visiting])
						{
							if (closed.Contains(neighbour))
							{
								continue;
							}

							open.Enqueue(neighbour);
							closed.Add(neighbour);
						}
					}
					groups++;
				}

				Console.WriteLine($"Part {i + 1}: {((i == 0) ? closed.Count : groups)}");
			}

			Console.Read();
		}
	}
}
