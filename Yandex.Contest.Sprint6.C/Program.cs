using System;
using System.Collections.Generic;
using System.Linq;

internal sealed class Node
{
	private readonly List<Node> _nodes = new();

	public Node(int index)
	{
		Index = index;
	}

	public int Index { get; }
	public IEnumerable<Node> Nodes => _nodes;

	public static void Add(Node left, Node right)
	{
		left._nodes.Add(right);
		right._nodes.Add(left);
	}
}

internal enum DFSColor
{
	White = 0,
	Grey = 1,
	Black = 2,
}

internal sealed class DFS
{
	private readonly Node[] _nodes;

	public DFS(Node[] nodes)
	{
		_nodes = nodes;
	}

	public IEnumerable<Node> GetNodes(int index)
	{
		var node = _nodes[index] ??= new Node(index);
		var colors = new DFSColor[_nodes.Length];
		var outNodes = new List<Node>();
		GetNodes(node, colors, outNodes);
		return outNodes;
	}

	private static void GetNodes(Node node, IList<DFSColor> colors, ICollection<Node> outNodes)
	{
		if (colors[node.Index] != DFSColor.White)
		{
			return;
		}

		outNodes.Add(node);
		colors[node.Index] = DFSColor.Grey;

		foreach (var innerNode in node.Nodes.OrderBy(item => item.Index))
		{
			GetNodes(innerNode, colors, outNodes);
		}

		colors[node.Index] = DFSColor.Black;
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var nm = Contest.GetArray(int.Parse);
		var n = nm[0];
		var m = nm[1];

		var nodes = new Node[n];
		for (var i = 0; i < m; i++)
		{
			var fromTo = Contest.GetArray(int.Parse);
			var from = fromTo[0] - 1;
			var to = fromTo[1] - 1;
			nodes[from] ??= new Node(from);
			nodes[to] ??= new Node(to);
			Node.Add(nodes[from], nodes[to]);
		}

		var index = Contest.GetValue(int.Parse);
		var dfs = new DFS(nodes);
		var arr = dfs.GetNodes(index - 1);
		arr.Select(node => node.Index + 1).Print();
	}
}

internal static class Contest
{
	public static string GetValue()
	{
		return Console.ReadLine()!;
	}

	public static T GetValue<T>(Func<string, T> func)
	{
		return func(GetValue());
	}

	public static IEnumerable<T> GetIEnumerable<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func);
	}

	public static List<T> GetList<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToList();
	}

	public static List<string> GetList(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(str => str, splitOptions);
	}

	public static T[] GetArray<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToArray();
	}

	public static string[] GetArray(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetArray(str => str, splitOptions);
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join(separator).Print();
	}

	public static void Print(this ValueType value)
	{
		Console.WriteLine(value);
	}

	public static void Print(this object obj)
	{
		Console.WriteLine(obj);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}
}