using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// https://contest.yandex.ru/contest/25070/run-report/80071094/
///
/// -- ПРИНЦИП РАБОТЫ --
///
/// Создаем граф указывая направления движения
/// B - от меньшего к большему
/// R - от большего к меньшему
/// Это позволяет создать циклы в графе
/// Далее для проверки оптимальности нужно убедится, что циклов нет
/// Для этого используем поиск в глубину и
/// если натыкаемся на серую вершину - значит есть цикл и граф не оптимален
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(E + V) - где E количество рёбер в графе, а V - количество вершин
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(E + V) - где E количество рёбер в графе, а V - количество вершин
/// 
/// </summary>
/// <summary>
/// Вершина графа
/// </summary>
internal sealed class Node
{
	/// <summary>
	/// Индекс вершины
	/// </summary>
	public readonly int Index;

	/// <summary>
	/// Исходяшие рёбра
	/// </summary>
	private readonly List<int> _edges = new();

	public Node(int index)
	{
		Index = index;
	}

	public IEnumerable<int> Edges => _edges;

	/// <summary>
	/// Добавить ребро
	/// </summary>
	/// <param name="node"></param>
	public void AddEdge(int node)
	{
		_edges.Add(node);
	}

	public override string ToString()
	{
		return $"{Index}";
	}
}

/// <summary>
/// Окраска вершины графа
/// </summary>
internal enum GraphColor
{
	/// <summary>
	/// Не посещался
	/// </summary>
	White = 0,
	/// <summary>
	/// В обработке
	/// </summary>
	Grey = 1,
	/// <summary>
	/// Посещён
	/// </summary>
	Black = 2,
}

/// <summary>
/// Тип пути
/// </summary>
internal enum EdgeType
{
	Undefined = -1,
	R = 0,
	B = 1,
}

/// <summary>
/// Граф
/// </summary>
internal sealed class Graph
{
	/// <summary>
	/// Внршины
	/// </summary>
	private readonly Node[] _nodes;

	/// <summary>
	/// Создание N вершинный граф 
	/// </summary>
	/// <param name="n"></param>
	public Graph(int n)
	{
		_nodes = Enumerable.Range(0, n)
			.Select(i => new Node(i))
			.ToArray();
	}

	/// <summary>
	/// Добавить ребро с типом ребра
	/// B - прямой путь
	/// R - инвертированный
	/// </summary>
	/// <param name="from"></param>
	/// <param name="to"></param>
	/// <param name="type"></param>
	public void Add(int from, int to, EdgeType type)
	{
		if (type == EdgeType.B)
		{
			_nodes[from].AddEdge(to);
		}

		if (type == EdgeType.R)
		{
			_nodes[to].AddEdge(from);
		}
	}

	/// <summary>
	/// Проверка оптимальности графа
	/// </summary>
	/// <returns></returns>
	public bool IsOptimal()
	{
		var colors = new GraphColor[_nodes.Length];

		for (var i = 0; i < _nodes.Length; i++)
		{
			var isLoopExist = IsLoopExist(i, colors);
			if (isLoopExist)
			{
				return false;
			}
		}

		return true;
	}

	private bool IsLoopExist(int nodeIndex, IList<GraphColor> colors)
	{
		if (colors[nodeIndex] == GraphColor.Grey)
		{
			return true;
		}

		if (colors[nodeIndex] == GraphColor.Black)
		{
			return false;
		}

		colors[nodeIndex] = GraphColor.Grey;

		foreach (var edgeIndex in _nodes[nodeIndex].Edges)
		{
			var isLoopExist = IsLoopExist(edgeIndex, colors);
			if (isLoopExist)
			{
				return true;
			}
		}

		colors[nodeIndex] = GraphColor.Black;
		return false;
	}

	/// <summary>
	/// Парсинг типа ребра
	/// </summary>
	/// <param name="c"></param>
	/// <returns></returns>
	public static EdgeType GetType(char c)
	{
		if (c == 'R')
		{
			return EdgeType.R;
		}

		if (c == 'B')
		{
			return EdgeType.B;
		}

		return EdgeType.Undefined;
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var n = Contest.GetValue(int.Parse);

		var graph = new Graph(n);
		for (var i = 0; i < n - 1; i++)
		{
			var railways = Contest.GetValue();

			for (var j = 0; j < railways.Length; j++)
			{
				var type = Graph.GetType(railways[j]);
				graph.Add(i, i + j + 1, type);
			}
		}

		if (graph.IsOptimal())
		{
			"YES".Print();
			return;
		}

		"NO".Print();
	}
}

/// <summary>
/// I/O методы
/// </summary>
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