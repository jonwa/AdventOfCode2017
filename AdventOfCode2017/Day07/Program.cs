using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
	class Program
	{
		static void Run(string[] input)
		{
			var tree = new Tree(input);
			var root = tree.GetRoot();
			var adjustedWeight = tree.GetAdjustedWeight();

			Console.WriteLine("-------------------");
			Console.WriteLine($"Part One: {root.name}");
			Console.WriteLine($"Part Two: {adjustedWeight}");
			Console.WriteLine("-------------------");
			Console.WriteLine();
		}

		static void Main(string[] args)
		{
			var input = File.ReadAllLines("../../input.txt");
			var testInput = new[] { "pbga (66)", "xhth (57)", "ebii (61)", "havc (66)", "ktlj (57)", "fwft (72) -> ktlj, cntj, xhth", "qoyq (66)", "padx (45) -> pbga, havc, qoyq", "tknk (41) -> ugml, padx, fwft", "jptl (61)", "ugml (68) -> gyxo, ebii, jptl", "gyxo (61)", "cntj (57)" };

			Console.WriteLine("Test");
			Run(testInput);

			Console.WriteLine("Solution");
			Run(input);

			Console.Read();
		}
	}
}
