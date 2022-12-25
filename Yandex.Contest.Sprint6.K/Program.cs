using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal sealed class Node
{
	private readonly List<Edge> _edges = new();

	public Node(int index)
	{
		Index = index;
		Directed = new GraphDirection(right => AddDirected(right), AddDirected);
		Undirected = new GraphDirection(right => AddUndirected(right), AddUndirected);
	}

	public int Index { get; }
	public IEnumerable<Edge> Edges => _edges;
	public GraphDirection Directed { get; }
	public GraphDirection Undirected { get; }

	private void AddDirected(Node right, int weight = 1)
	{
		_edges.Add(new Edge(right, weight));
	}

	private void AddUndirected(Node right, int weight = 1)
	{
		AddDirected(right, weight);
		right.AddDirected(this, weight);
	}
}

internal sealed class Edge
{
	public Edge(Node node, int weight)
	{
		Node = node;
		Weight = weight;
	}

	public Node Node { get; }
	public int Weight { get; }
}

internal sealed class GraphDirection
{
	private readonly Action<Node> _addAction;
	private readonly Action<Node, int> _addWeightAction;

	public GraphDirection(Action<Node> addAction, Action<Node, int> addWeightAction)
	{
		_addAction = addAction;
		_addWeightAction = addWeightAction;
	}

	public void Add(Node right) => _addAction?.Invoke(right);
	public void Add(Node right, int weight) => _addWeightAction?.Invoke(right, weight);
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
	private readonly Node[] _nodes;
	private readonly Dictionary<Node, GraphAddition> _additions = new();
	private readonly GraphTimeController _timeController = new();

	public GraphAdditionController(Node[] nodes)
	{
		_nodes = nodes;
	}

	public GraphAddition GetAddition(Node node, int connectivity = -1)
	{
		if (_additions.ContainsKey(node) == false)
		{
			_additions[node] = new GraphAddition(node, _timeController, connectivity);
		}

		return _additions[node];
	}

	public GraphAddition GetAddition(int index, int connectivity = -1)
	{
		return GetAddition(_nodes[index] ??= new Node(index), connectivity);
	}

	public GraphAddition GetAddition(Edge edge, GraphAddition previous)
	{
		return GetAddition(edge.Node, previous.Connectivity)
			.SetPrevious(previous)
			.SetDist(previous.Dist + edge.Weight);
	}

	public GraphAddition GetDeikstraAddition(Edge edge, GraphAddition previous)
	{
		var addition = GetAddition(edge.Node, previous.Connectivity);
		if (addition.IsWhite)
		{
			return addition.SetDeikstra(previous, previous.Dist + edge.Weight);
		}

		if (addition.IsBlack)
		{
			return addition;
		}

		var newDist = previous.Dist + edge.Weight;
		if (addition.Dist <= newDist)
		{
			return addition;
		}

		return addition.SetDeikstra(previous, newDist);
	}

	public IEnumerable<GraphAddition> GetInnerAdditions(GraphAddition addition)
	{
		return addition.Node.Edges
			.OrderBy(edge => edge.Node.Index)
			.Select(edge => GetAddition(edge, addition));
	}

	public IEnumerable<GraphAddition> GetDeikstraAdditions(GraphAddition addition)
	{
		return addition.Node.Edges
			.Select(edge => GetDeikstraAddition(edge, addition))
			.Where(addition => addition.IsBlack == false);
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
	public int Connectivity { get; }
	public int? InTime { get; private set; }
	public int? OutTime { get; private set; }
	public GraphAddition Previous { get; private set; }
	public int Dist { get; private set; } = -1;
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

	public GraphAddition SetPrevious(GraphAddition addition)
	{
		if (Previous != null)
		{
			return this;
		}

		Previous = addition;
		return this;
	}

	public GraphAddition SetDist(int dist)
	{
		if (Dist >= 0)
		{
			return this;
		}

		Dist = dist;
		return this;
	}

	public GraphAddition SetDeikstra(GraphAddition addition, int dist)
	{
		Previous = addition;
		Dist = dist;
		return this;
	}

	public GraphAddition NextState()
	{
		if (IsWhite)
		{
			_color = DfsColor.Grey;
			InTime = _timeController.GetTime();
			return this;
		}

		if (IsGrey)
		{
			_color = DfsColor.Black;
			OutTime = _timeController.GetTime();
			return this;
		}

		return this;
	}
}

internal sealed class Bfs
{
	private readonly Node[] _nodes;

	public Bfs(Node[] nodes)
	{
		_nodes = nodes;
	}

	public IEnumerable<IEnumerable<int>> GetGraph()
	{
		for (var i = 0; i < _nodes.Length; i++)
		{
			yield return GetGraph(i);
		}
	}

	private IEnumerable<int> GetGraph(int index)
	{
		var additionsController = new GraphAdditionController(_nodes);

		var additions = Enumerable.Range(0, _nodes.Length)
			.Select(i => additionsController.GetAddition(i))
			.ToArray();

		var addition = additionsController.GetAddition(index)
			.SetDist(0)
			.NextState();

		GetGraph(addition, additionsController);

		while (true)
		{
			var grayAdditions = additions
				.Where(item => item.IsGrey)
				.ToArray();

			if (grayAdditions.Length == 0)
			{
				break;
			}

			addition = grayAdditions[0];
			for (var i = 1; i < grayAdditions.Length; i++)
			{
				if (grayAdditions[i].Dist < addition.Dist)
				{
					addition = grayAdditions[i];
				}
			}

			GetGraph(addition, additionsController);
		}

		return _nodes
			.Select((node, index) => additionsController.GetAddition(index))
			.Select(addition => addition.Dist);
	}

	private static void GetGraph(GraphAddition addition, GraphAdditionController additions)
	{
		foreach (var innerAddition in additions.GetDeikstraAdditions(addition))
		{
			innerAddition.NextState();
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
			var weight = fromTo[2];

			nodes[from] ??= new Node(from);
			nodes[to] ??= new Node(to);
			nodes[from].Undirected.Add(nodes[to], weight);
		}

		var bfs = new Bfs(nodes);
		var graph = bfs.GetGraph();
		graph.Select(item => item.Join("\t")).Print("\n\n");
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