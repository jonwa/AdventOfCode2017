using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
	class Program
	{
		static void Main(string[] args)
		{
			var layers = new int[99];
			foreach(var line in File.ReadAllLines("../../input.txt"))
			{
				var split = line.Split(':');
				layers[int.Parse(split[0])] = int.Parse(split[1]);
			}

			int severity = 0, delay = 0;

			for (int i = 0; i < layers.Length; ++i)
			{
				if (i % (2 * (layers[i] - 1)) == 0)
				{
					severity += i * layers[i];
				}
			}

			for(;;)
			{
				bool caught = false;
				for (int i = 0; i < layers.Length; ++i)
				{
					if (layers[i] > 0 && (delay + i) % (2 * (layers[i] - 1)) == 0)
					{
						caught = true;
						delay++;
						break;
					}
				}

				if (!caught)
				{
					break;
				}
			}

			Console.WriteLine($"Part One: {severity}");
			Console.WriteLine($"Part Two: {delay}");
			Console.ReadLine();
		}
	}
}
