using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
	public class Tree
	{
		public List<Node> nodes = new List<Node>();

		public class Node
		{
			public string name { get; set; }
			public int weight { get; set; }
			public int totalWeight { get => weight + children.Sum(c => c.totalWeight); }
			public List<Node> children { get; set; }
		}

		public Tree(string[] input)
		{
			Dictionary<string, Node> nodesDict = new Dictionary<string, Node>();
			Dictionary<string, string[]> childDict = new Dictionary<string, string[]>();

			Action<string, int> addNode = (string name, int weight) =>
			{
				nodesDict[name] = new Node()
				{
					name = name,
					weight = weight,
					children = new List<Node>()
				};
			};

			foreach (var item in input)
			{
				if (item.Contains("->"))
				{
					var split = item.Split(new string[] { "->" }, StringSplitOptions.None);
					var innerSplit = split[0].Trim().Split('(');
					var name = innerSplit[0].Trim();
					var weight = int.Parse(innerSplit[1].Split(')')[0]);
					var children = split[1].Split(',').Select(c => c.Trim()).ToArray();
					addNode(name, weight);
					childDict[name] = children;
				}
				else
				{
					var split = item.Trim().Split('(');
					var name = split[0].Trim();
					var weight = int.Parse(split[1].Trim().Split(')')[0]);
					addNode(name, weight);
				}
			}

			foreach (var kvp in childDict)
			{
				foreach (var child in kvp.Value)
				{
					nodesDict[kvp.Key].children.Add(nodesDict[child]);
				}
			}

			nodes.AddRange(nodesDict.Values);
		}

		public Node GetRoot()
		{
			var root = nodes.OrderBy(n => n.totalWeight).Last();
			return root;
		}

		public int GetAdjustedWeight()
		{
			Node parent = null;
			Node source = GetErrorBranch();
			var distinct = source.children.GroupBy(n => n.totalWeight).Last().ToList();
			while(distinct.Count == 1)
			{
				parent = source;
				source = distinct.First();
				distinct = source.children.GroupBy(n => n.totalWeight).Last().ToList();
			} while (distinct.Count == 1);

			if (parent == null)
				parent = nodes.FirstOrDefault(n => n.children.Contains(source));

			var sibling = parent.children.FirstOrDefault(n => n != source);
			var diff = Math.Abs(source.totalWeight - sibling.totalWeight);
			var result = source.weight - diff;
			return result;
		}

		private Node GetErrorBranch()
		{
			foreach (var node in nodes.Where(n => n.children.Count > 0))
			{
				var distinct = node.children.GroupBy(n => n.totalWeight).First().ToList();
				if (distinct.Count == 1)
				{
					return distinct.First();
				}
			}
			return null;
		}
	}
}
