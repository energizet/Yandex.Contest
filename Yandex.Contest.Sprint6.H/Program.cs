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

	public GraphDirection Directed => new(AddDirected);
	public GraphDirection Undirected => new(AddUndirected);

	private void AddDirected(Node right)
	{
		_nodes.Add(right);
	}

	private void AddUndirected(Node right)
	{
		AddDirected(right);
		right.AddDirected(this);
	}
}

internal sealed class GraphDirection
{
	private readonly Action<Node> _addAction;

	public GraphDirection(Action<Node> addAction)
	{
		_addAction = addAction;
	}

	public void Add(Node right) => _addAction?.Invoke(right);
}

internal sealed class GraphTimeController
{
	private int _time = 0;

	public int GetTime()
	{
		return _time++;
	}
}

internal sealed class GraphAdditionController
{
	private readonly Dictionary<Node, GraphAddition> _additions = new();
	private readonly GraphTimeController _timeController = new();

	public GraphAddition GetAddition(Node node)
	{
		if (_additions.ContainsKey(node) == false)
		{
			_additions[node] = new GraphAddition(node, _timeController);
		}

		return _additions[node];
	}
}

internal sealed class GraphAddition
{
	public Node Node { get; }
	public int? InTime { get; private set; }
	public int? OutTime { get; private set; }
	private DfsColor _color = DfsColor.White;
	private readonly GraphTimeController _timeController;

	public GraphAddition(Node node, GraphTimeController timeController)
	{
		Node = node;
		_timeController = timeController;
	}

	public bool IsWhite()
	{
		return _color == DfsColor.White;
	}

	public bool IsGrey()
	{
		return _color == DfsColor.Grey;
	}

	public bool IsBlack()
	{
		return _color == DfsColor.Black;
	}

	public void NextState()
	{
		if (IsWhite())
		{
			_color = DfsColor.Grey;
			InTime = _timeController.GetTime();
			return;
		}

		if (IsGrey())
		{
			_color = DfsColor.Black;
			OutTime = _timeController.GetTime();
			return;
		}
	}
}

internal enum DfsColor
{
	White = 0,
	Grey = 1,
	Black = 2,
}

internal sealed class Dfs
{
	public IEnumerable<GraphAddition> GetNodes(Node node)
	{
		var additions = new GraphAdditionController();

		var outNodes = new List<GraphAddition>();
		GetNodes(additions.GetAddition(node), additions, outNodes);
		return outNodes.OrderBy(item => item.Node.Index);
	}

	private static void GetNodes(GraphAddition addition, GraphAdditionController additions,
		ICollection<GraphAddition> outNodes)
	{
		if (addition.IsWhite() == false)
		{
			return;
		}

		outNodes.Add(addition);
		addition.NextState();

		foreach (var innerNode in addition.Node.Nodes.OrderBy(item => item.Index))
		{
			GetNodes(additions.GetAddition(innerNode), additions, outNodes);
		}

		addition.NextState();
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
			nodes[from].Directed.Add(nodes[to]);
		}

		var dfs = new Dfs();
		var arr = dfs.GetNodes(nodes[0] ??= new Node(0));
		arr.Select(times => $"{times.InTime} {times.OutTime}").Print("\n");
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