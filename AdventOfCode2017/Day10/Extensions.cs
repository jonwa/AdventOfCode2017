using System.Linq;
using System.Text;

namespace Day10
{
	public static class Extensions
	{
		private static void Reverse(this byte[] A, int index, int count)
		{
			for (int i = 0; i < count / 2; ++i)
			{
				int a = (index + i) % A.Length;
				int b = (index + ((count - 1) - i)) % A.Length;
				
				var tmp = A[a];
				A[a] = A[b];
				A[b] = tmp;
			}
		}

		private static byte[] ToKnotHash(this byte[] input, int rounds = 1)
		{
			byte length;
			int currentPos = 0, skipSize = 0;
			var result = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

			for (int i = 0; i < rounds; ++i)
			{
				for (int j = 0; j < input.Length; ++j)
				{
					length = input[j];
					if (length <= result.Length)
					{
						result.Reverse(currentPos, length);
						currentPos = (currentPos + (length + skipSize)) % result.Length;
						skipSize++;
					}
				}
			}

			return result;
		}

		public static byte[] ToKnotHash(this string input)
		{
			var inputArray = input.Split(',').Select(s => byte.Parse(s)).ToArray();
			return inputArray.ToKnotHash();
		}

		public static string ToKnotHash64(this string input)
		{
			var inputArray = ASCIIEncoding.ASCII.GetBytes(input)
				.Concat(new byte[] { 17, 31, 73, 47, 23 })
				.ToArray();

			var denseHash = new long[16];
			var sparseHash = inputArray.ToKnotHash(64);
			for (int i = 0, j = 0; i < sparseHash.Length; i += 16, ++j)
			{
				for (int k = 0; k < 16; ++k)
				{
					denseHash[j] ^= sparseHash[i + k];
				}
			}

			return string.Join("", denseHash.ToList().ConvertAll(i => string.Format("{0:x2}", i)));
		}
	}
}
