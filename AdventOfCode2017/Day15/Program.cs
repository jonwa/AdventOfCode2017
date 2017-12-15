using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
	public static class Extensions
	{
		public static string ToBinary(this long l)
		{
			var A = new char[32];
			int i = 0, j = 31;

			while (i < 32)
			{
				if ((l & (1 << i)) != 0) A[j--] = '1';
				else A[j--] = '0';
				i++;
			}

			return new string(A);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			for (int itr = 0; itr < 2; ++itr)
			{
				int match = 0;
				long aValue = 783, bValue = 325;
				string aBinary = string.Empty, bBinary = string.Empty;

				for (int i = 0; i < (itr == 0 ? 40000000 : 5000000); ++i)
				{
					if (itr == 0)
					{
						aValue = (aValue * 16807) % 2147483647;
						bValue = (bValue * 48271) % 2147483647;
					}
					else
					{
						do
						{
							aValue = (aValue * 16807) % 2147483647;
						} while (aValue % 4 != 0);

						do
						{
							bValue = (bValue * 48271) % 2147483647;
						} while (bValue % 8 != 0);
					}

					aBinary = aValue.ToBinary();
					bBinary = bValue.ToBinary();

					if (string.Compare(aBinary, 16, bBinary, 16, 16) == 0)
					{
						match++;
					}
				}
				Console.WriteLine($"Part {itr + 1}: {match}");
			}
			
			Console.Read();
		}
	}
}
