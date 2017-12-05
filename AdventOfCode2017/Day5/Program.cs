using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
	class Program
	{
		interface ICPU
		{
			bool Test();
			int Jumps(int[] A);
		}

		class PartOne : ICPU
		{
			public bool Test()
			{
				var A = new[] { 0, 3, 0, 1, -3 };
				try
				{
					return Jumps(A) == 5;
				}
				catch (Exception e)
				{
					return false;
				}
			}

			public int Jumps(int[] A)
			{
				int i = 0, j = 0, k = 0;
				while (i < A.Length)
				{
					k++;
					j = i;
					i += A[i];
					if (i >= A.Length) break;
					A[j]++;
				}
				return k;
			}
		}

		class PartTwo : ICPU
		{
			public bool Test()
			{
				var A = new[] { 0, 3, 0, 1, -3 };
				try
				{
					return Jumps(A) == 10;
				}
				catch (Exception e)
				{
					return false;
				}
			}

			public int Jumps(int[] A)
			{
				int i = 0, j = 0, k = 0;
				while (i < A.Length)
				{
					k++;
					j = i;
					i += A[i];
					if (i >= A.Length) break;
					A[j] += A[j] >= 3 ? -1 : 1;
				}
				return k;
			}
		}
	
		static int Run<T>(int[] A) where T : ICPU, new()
		{
			var o = new T();
			var t = o.Test();
			Console.WriteLine(t ? $"{o.GetType().Name} - Succeeded." : $"{o.GetType().Name} - Failed.");
			return o.Jumps(A);
		}

		static void Main(string[] args)
		{
			var input = File.ReadAllLines("../../input.txt");
			var A = Array.ConvertAll(input, s => int.Parse(s));
			var B = Array.ConvertAll(input, s => int.Parse(s));

			Console.WriteLine($"Solution Part One: { Run<PartOne>(A) }");
			Console.WriteLine($"Solution Part Two: { Run<PartTwo>(B) }");
			Console.Read();
		}
	}
}
