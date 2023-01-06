using System;
using System.Collections.Generic;
using System.Linq;

/// https://contest.yandex.ru/contest/25070/run-report/80017020/
///
/// -- ПРИНЦИП РАБОТЫ --
///
/// При заполнении графа - создаются вершины и заполняются исходящие рёбра
/// Реализация кучи взята из пятого спринта и переделана на обобщённую кучу
///
/// При нахождении максимальных рёбер создаётся массив посещёных вершин и куча
/// Берётся случайная вершина и добавляется в посещённые, а исходящие вершины в кучу
/// В цикле получаем самое большое ребро из кучи и добавляем его в посещённые,
/// а вес добавляем к общему весу и исходящие рёбра в кучу
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(E * log(V)) - где E количество рёбер в графе, а V - количество вершин
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(E + V) - где E количество рёбер в графе, а V - количество вершин
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
	private readonly List<Edge> _edges = new();

	public Node(int index)
	{
		Index = index;
	}

	public IEnumerable<Edge> Edges => _edges;

	/// <summary>
	/// Добавить ребро и его вес
	/// </summary>
	/// <param name="to"></param>
	/// <param name="weight"></param>
	/// <returns></returns>
	public Node AddEdge(Node to, int weight)
	{
		_edges.Add(new Edge(to, weight));
		return this;
	}

	public override string ToString()
	{
		return $"{Index}";
	}
}

/// <summary>
/// Ребро графа
/// </summary>
internal sealed class Edge : IComparable<Edge>
{
	/// <summary>
	/// Вершина на которую указывает ребро
	/// </summary>
	public readonly Node Node;

	/// <summary>
	/// Вес ребра
	/// </summary>
	public readonly int Weight;

	public Edge(Node node, int weight)
	{
		Node = node;
		Weight = weight;
	}

	public int CompareTo(Edge other)
	{
		return Weight.CompareTo(other.Weight);
	}

	public override string ToString()
	{
		return $"{Node} - {Weight}";
	}
}

/// <summary>
/// Куча
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class Heap<T> where T : IComparable<T>
{
	/// <summary>
	/// Дерево кучи
	/// </summary>
	private readonly List<T> _items = new() {default};

	/// <summary>
	/// Вставка элемента
	/// </summary>
	/// <param name="item"></param>
	public void Push(T item)
	{
		_items.Add(item);
		SiftUp(_items.Count - 1);
	}

	/// <summary>
	/// Множественная вставка
	/// </summary>
	/// <param name="items"></param>
	public void Push(IEnumerable<T> items)
	{
		foreach (var item in items)
		{
			Push(item);
		}
	}

	/// <summary>
	/// Получить элемент с вершины кучи
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool TryPop(out T item)
	{
		if (_items.Count < 2)
		{
			item = default;
			return false;
		}

		item = _items[1];
		_items[1] = _items[^1];
		_items.RemoveAt(_items.Count - 1);

		SiftDown(1);
		return true;
	}

	private int SiftUp(int idx)
	{
		if (idx == 1)
		{
			return 1;
		}

		var parentIdx = idx / 2;
		var item = _items[idx];
		var parent = _items[parentIdx];

		if (item.CompareTo(parent) <= 0)
		{
			return idx;
		}

		_items[parentIdx] = item;
		_items[idx] = parent;
		return SiftUp(parentIdx);
	}

	private int SiftDown(int idx)
	{
		if (idx >= _items.Count)
		{
			return 0;
		}

		var leftIdx = idx * 2;
		var rightIdx = idx * 2 + 1;

		var item = _items[idx];

		if (leftIdx >= _items.Count)
		{
			return idx;
		}

		var left = _items[leftIdx];

		if (rightIdx >= _items.Count)
		{
			if (left.CompareTo(item) <= 0)
			{
				return idx;
			}

			_items[idx] = left;
			_items[leftIdx] = item;
			return leftIdx;
		}

		var right = _items[rightIdx];

		var maxIdx = idx;
		if (left.CompareTo(_items[maxIdx]) > 0)
		{
			maxIdx = leftIdx;
		}

		if (right.CompareTo(_items[maxIdx]) > 0)
		{
			maxIdx = rightIdx;
		}

		if (maxIdx == idx)
		{
			return idx;
		}

		_items[idx] = _items[maxIdx];
		_items[maxIdx] = item;
		return SiftDown(maxIdx);
	}
}

/// <summary>
/// Граф
/// </summary>
internal sealed class Graph
{
	/// <summary>
	/// Вершины
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
	/// Добавить ребро
	/// </summary>
	/// <param name="from"></param>
	/// <param name="to"></param>
	/// <param name="weight"></param>
	/// <returns></returns>
	public Graph Add(int from, int to, int weight)
	{
		if (from == to)
		{
			return this;
		}

		_nodes[from].AddEdge(_nodes[to], weight);

		return this;
	}

	/// <summary>
	/// Добавить не ориентированное ребро
	/// </summary>
	/// <param name="from"></param>
	/// <param name="to"></param>
	/// <param name="weight"></param>
	/// <returns></returns>
	public Graph AddUndirected(int from, int to, int weight)
	{
		return Add(from, to, weight)
			.Add(to, from, weight);
	}

	/// <summary>
	/// Получить максимальный вес остова
	/// </summary>
	/// <param name="maxWeight"></param>
	/// <returns></returns>
	public bool TryGetMaxWeight(out int maxWeight)
	{
		if (_nodes.Length == 0)
		{
			maxWeight = 0;
			return true;
		}

		var added = new HashSet<Node>();
		var heap = new Heap<Edge>();

		var node = _nodes.First();
		added.Add(node);
		heap.Push(node.Edges);

		maxWeight = 0;
		while (added.Count < _nodes.Length)
		{
			var isPop = heap.TryPop(out var edge);
			if (isPop == false)
			{
				break;
			}

			if (added.Contains(edge.Node))
			{
				continue;
			}

			maxWeight += edge.Weight;
			added.Add(edge.Node);
			heap.Push(edge.Node.Edges);
		}

		return added.Count == _nodes.Length;
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var nm = Contest.GetArray(int.Parse);
		var n = nm[0];
		var m = nm[1];

		var graph = new Graph(n);
		for (var i = 0; i < m; i++)
		{
			var fromTo = Contest.GetArray(int.Parse);
			var from = fromTo[0] - 1;
			var to = fromTo[1] - 1;
			var weight = fromTo[2];

			graph.AddUndirected(from, to, weight);
		}

		if (graph.TryGetMaxWeight(out var maxWeight))
		{
			Console.WriteLine(maxWeight);
			return;
		}

		Console.WriteLine("Oops! I did it again");
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