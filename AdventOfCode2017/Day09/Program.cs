using System;
using System.IO;

namespace Day9
{
	public static class Extensions
	{
		public static int[] ReadStream(this string stream)
		{
			int indent = 0, score = 0, garbageCount = 0;
			bool ignore = false, garbage = false;

			foreach (var c in stream)
			{
				if (ignore)
				{
					ignore = false;
					continue;
				}

				if (garbage)
				{
					switch (c)
					{
						case '!':
							ignore = true;
							continue;
						case '>':
							garbage = false;
							continue;
						default:
							garbageCount++;
							continue;
					}
				}
				else
				{
					switch (c)
					{
						case '<':
							garbage = true;
							continue;
						case '{':
							indent++;
							continue;
						case '}':
							score += indent;
							indent--;
							continue;
					}
				}
			}

			return new [] { score, garbageCount };
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var input = File.ReadAllText("../../input.txt");
			var result = input.ReadStream();
			Console.WriteLine($"Part One: {result[0]}");
			Console.WriteLine($"Part Two: {result[1]}");
			Console.Read();
		}
	}
}
