﻿using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal sealed class Node
{
	private readonly List<Node> _nodes = new();

	public Node(int index)
	{
		Index = index;
		Directed = new GraphDirection(right => AddDirected(right));
		Undirected = new GraphDirection(right => AddUndirected(right));
	}

	public int Index { get; }
	public IEnumerable<Node> Nodes => _nodes;
	public GraphDirection Directed { get; }
	public GraphDirection Undirected { get; }

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

	public GraphAddition GetAddition(Node node, GraphAddition previous)
	{
		var innerAddition = GetAddition(node, previous.Connectivity);
		innerAddition.SetPrevious(previous);
		innerAddition.SetLength(previous.Length + 1);
		return innerAddition;
	}

	public IEnumerable<GraphAddition> GetInnerAdditions(GraphAddition addition)
	{
		return addition.Node.Nodes
			.OrderBy(node => node.Index)
			.Select(node => GetAddition(node, addition));
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
	public int Length { get; private set; } = 0;
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

	public void SetPrevious(GraphAddition addition)
	{
		if (Previous != null)
		{
			return;
		}

		Previous = addition;
	}

	public void SetLength(int length)
	{
		if (Length != 0)
		{
			return;
		}

		Length = length;
	}

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

internal sealed class Bfs
{
	private readonly Node[] _nodes;

	public Bfs(Node[] nodes)
	{
		_nodes = nodes;
	}

	public int GetMaxLength(int index)
	{
		var additions = new GraphAdditionController(_nodes);

		var planned = new Queue<GraphAddition>();
		var addition = additions.GetAddition(index);
		addition.NextState();
		planned.Enqueue(addition);

		return GetMaxLength(planned, additions);
	}

	private static int GetMaxLength(Queue<GraphAddition> planned, GraphAdditionController additions)
	{
		var addition = default(GraphAddition);
		while (planned.Count > 0)
		{
			addition = planned.Dequeue();

			foreach (var innerAddition in additions.GetInnerAdditions(addition))
			{
				if (innerAddition.IsWhite == false)
				{
					continue;
				}

				innerAddition.NextState();
				planned.Enqueue(innerAddition);
			}

			addition.NextState();
		}

		return addition?.Length ?? -1;
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

		var bfs = new Bfs(nodes);
		var startIndex = Contest.GetValue(int.Parse) - 1;
		bfs.GetMaxLength(startIndex).Print();
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