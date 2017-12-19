using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
	class Program
	{
		static void Main(string[] args)
		{
			var A = new List<int>() { 0 };
			int steps = 367, index = 0, val = 0;

			for (int i = 1; i <= 2017; ++i)
			{
				index = (index + steps) % i + 1;
				A.Insert(index, i);
			}

			Console.WriteLine($"Part One: {A[index + 1]}");
			index = 0;

			for (int i = 1; i <= 50000000; ++i)
			{
				index = (index + steps) % i + 1;
				if (index == 1) val = i;
			}

			Console.WriteLine($"Part Two: {val}");
			Console.Read();
		}
	}
}
