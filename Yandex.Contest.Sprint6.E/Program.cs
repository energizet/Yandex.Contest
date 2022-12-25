using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

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

	public GraphAddition GetAddition(Node node, int connectivity = -1)
	{
		if (_additions.ContainsKey(node) == false)
		{
			_additions[node] = new GraphAddition(node, _timeController, connectivity);
		}

		return _additions[node];
	}

	public IEnumerable<GraphAddition> GetInnerAdditions(GraphAddition addition)
	{
		return addition.Node.Nodes
			.OrderBy(node => node.Index)
			.Select(node => GetAddition(node, addition.Connectivity));
	}
}

internal enum DfsColor
{
	White = 0,
	Grey = 1,
	Black = 2,
}

internal sealed class GraphAddition
{
	public Node Node { get; }
	public int? InTime { get; private set; }
	public int? OutTime { get; private set; }
	public int Connectivity { get; }
	private DfsColor _color = DfsColor.White;
	private readonly GraphTimeController _timeController;

	public GraphAddition(Node node, GraphTimeController timeController, int connectivity = -1)
	{
		Node = node;
		_timeController = timeController;
		Connectivity = connectivity;
	}

	public bool IsWhite => _color == DfsColor.White;

	public bool IsGrey => _color == DfsColor.Grey;

	public bool IsBlack => _color == DfsColor.Black;

	public void NextState()
	{
		if (IsWhite)
		{
			_color = DfsColor.Grey;
			InTime = _timeController.GetTime();
			return;
		}

		if (IsGrey)
		{
			_color = DfsColor.Black;
			OutTime = _timeController.GetTime();
			return;
		}
	}
}

internal sealed class Dfs
{
	private readonly Node[] _nodes;

	public Dfs(Node[] nodes)
	{
		_nodes = nodes;
	}

	public IEnumerable<GraphAddition> GetGraph()
	{
		var additions = new GraphAdditionController();

		var outNodes = new List<GraphAddition>();
		var connectivity = 0;
		for (var i = 0; i < _nodes.Length; i++)
		{
			var node = _nodes[i] ??= new Node(i);
			var addition = additions.GetAddition(node, connectivity);
			if (addition.IsWhite == false)
			{
				continue;
			}

			connectivity++;
			GetGraph(addition, additions, outNodes);
		}

		return outNodes;
	}

	private static void GetGraph(GraphAddition addition, GraphAdditionController additions,
		ICollection<GraphAddition> outNodes)
	{
		if (addition.IsWhite == false)
		{
			return;
		}

		outNodes.Add(addition);
		addition.NextState();

		foreach (var innerAddition in additions.GetInnerAdditions(addition))
		{
			GetGraph(innerAddition, additions, outNodes);
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
			nodes[from].Undirected.Add(nodes[to]);
		}

		var dfs = new Dfs(nodes);
		var arr = dfs.GetGraph();
		var grouped = arr
			.GroupBy(item => item.Connectivity, (key, group) =>
			{
				var graphAdditions = group.ToArray();

				return new
				{
					MinIndex = graphAdditions.Min(item => item.Node.Index),
					Group = graphAdditions.OrderBy(item => item.Node.Index),
				};
			})
			.OrderBy(item => item.MinIndex)
			.Select(item => item.Group.Select(item2 => item2.Node.Index + 1))
			.Select(item => item.Join())
			.ToArray();
		grouped.Length.Print();
		grouped.Print("\n");
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