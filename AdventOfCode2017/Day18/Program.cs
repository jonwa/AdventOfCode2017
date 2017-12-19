using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
	class Program
	{
		static void Main(string[] args)
		{
			var instructions = File.ReadAllLines("../../input.txt");

			var p0 = new Program(instructions);
			Console.WriteLine($"Part One: { p0.Run() }");

			var p1 = new Program(0L, instructions);
			var p2 = new Program(1L, instructions);
			p1.otherQueue = p2.queue;
			p2.otherQueue = p1.queue;

			while(true)
			{
				long r1 = 0, r2 = 0;
				if ((r1 = p1.DoOne()) != 0 && (r2 = p2.DoOne()) != 0)
				{
					Console.WriteLine($"Part Two: { r2 }");
					break;
				}
			}

			Console.Read();
		}

		public Program(string[] instructions)
		{
			this.queue = new Queue<long>();
			this.instructions = instructions;
			this.registers = new Dictionary<char, long>();
			for (char c = 'a'; c <= 'z'; ++c)
				registers[c] = 0;
		}

		public Program(long id, string[] instructions)
			: this(instructions)
		{
			itr = 0;
			sendCount = 0;
			registers['p'] = id;
		}

		long itr = 0;
		long sendCount = 0;
		string[] instructions;
		Queue<long> queue, otherQueue;
		Dictionary<char, long> registers;

		public long Run()
		{
			long value = 0L, result = 0L;

			for (int i = 0; i < instructions.Length; ++i)
			{
				var instruction = instructions[i].Split(' ');
				var register = instruction[1][0];

				if (instruction.Length == 3)
				{
					value = (registers.ContainsKey(instruction[2][0])) 
						? registers[instruction[2][0]] : long.Parse(instruction[2]);
				}

				switch (instruction[0])
				{
					case "set": registers[register] = value; break;
					case "add": registers[register] += value; break;
					case "mul": registers[register] *= value; break;
					case "mod": registers[register] %= value; break;

					case "snd":
						queue.Enqueue(registers[register]);
						break;

					case "rcv":
						if (registers[register] != 0)
						{
							result = queue.Last();
						}
						break;

					case "jgz":
						if ((registers.ContainsKey(register) && registers[register] > 0)
							|| char.GetNumericValue(register) > 0)
						{
							i += (int)value - 1;
						}
						break;
				}

				if (result != 0)
				{
					return result;
				}
			}

			return 0L;
		}

		public long DoOne()
		{
			long value = 0;
			var instruction = instructions[itr].Split(' ');
			var register = instruction[1][0];

			if (instruction.Length == 3)
			{
				value = (registers.ContainsKey(instruction[2][0]))
					? registers[instruction[2][0]] : long.Parse(instruction[2]);
			}

			switch (instruction[0])
			{
				case "set": registers[register] = value; break;
				case "add": registers[register] += value; break;
				case "mul": registers[register] *= value; break;
				case "mod": registers[register] %= value; break;

				case "snd":
					otherQueue.Enqueue(registers[register]);
					sendCount++;
					break;

				case "rcv":
					if (queue.Count > 0)
					{
						registers[register] = queue.Dequeue();
					}
					else
					{
						return sendCount;
					}
					break;

				case "jgz":
					if ((registers.ContainsKey(register) && registers[register] > 0) 
						|| char.GetNumericValue(register) > 0)
					{
						itr += value - 1;
					}
					break;
			}

			++itr;
			return 0;
		}
	}
}
