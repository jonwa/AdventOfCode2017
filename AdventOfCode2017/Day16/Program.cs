using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
	class Program
	{
		static void Main(string[] args)
		{
			var A = new StringBuilder("abcdefghijklmnop");
			var moves = File.ReadAllText("../../input.txt").Split(',');

			for (int i = 0; i < 1000000000 % 42; ++i)
			{
				foreach (var move in moves)
				{
					if (move.Contains("/"))
					{
						if (move.StartsWith("x"))
						{
							var split = move.TrimStart('x').Split('/');
							var index1 = int.Parse(split[0]);
							var index2 = int.Parse(split[1]);
							var tmp = A[index1];
							A[index1] = A[index2];
							A[index2] = tmp;
						}
						else
						{
							var split = move.Split('/');
							var index1 = A.ToString().IndexOf(Convert.ToChar(split[0][1]));
							var index2 = A.ToString().IndexOf(Convert.ToChar(split[1][0]));
							var tmp = A[index1];
							A[index1] = A[index2];
							A[index2] = tmp;
						}
					}
					else
					{
						var steps = int.Parse(move.TrimStart('s'));
						var tmp = A.ToString();
						var sub = tmp.Substring(tmp.Length - steps, steps);
						A.Replace(sub, "");
						A.Insert(0, sub);
					}
				}

				if (i == 0)
				{
					Console.WriteLine($"Part One: {A} ");
				}
			}

			Console.WriteLine($"Part Two: {A}");
			Console.Read();
		}
	}
}
