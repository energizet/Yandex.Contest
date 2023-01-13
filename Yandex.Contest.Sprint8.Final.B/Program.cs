using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

/// <summary>
/// https://contest.yandex.ru/contest/26133/run-report/80543068/
/// 
/// -- ПРИНЦИП РАБОТЫ --
///
/// По словарным словам создаётся бор
/// Для проверки создадим очередь и добавим нулевой индекс
/// В очереди будут хранится индексы с которых должно начинаться новое слово
/// Проверять фразу начиная с индекса из очереди
/// На каждой итерации переходим на следующий узел
/// Добавляем индексы на которых нашли терминальный узел в очередь
/// Если на какой-то итерации мы дошли до конца строки - значит фразу разделить можно
/// Если в очереди нет индексов - значит фразу разделить нельзя
/// Для того чтобы не проверять один и тот же стартовый раз повторно
/// Сохраняем их в список, если повторно наткнёмся на такой - можем не проверять его
///		ведь раньше мы его уже проверяли
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(n) - n - длина проверяемой строки
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(L) - L - длина всех словарных слов
/// 
/// </summary>
internal class Trie
{
	private readonly Node _root = new();

	/// <summary>
	/// Вставляет рёбра в дерево
	/// </summary>
	/// <param name="word"></param>
	public void Add(string word)
	{
		var currentNode = _root;
		foreach (var letter in word)
		{
			if (currentNode.Edges.TryGetValue(letter, out var node))
			{
				currentNode = node;
				continue;
			}

			currentNode = currentNode.Add(letter);
		}

		currentNode.MarkTerminal();
	}

	/// <summary>
	/// Проверяет можно ли разбить строку словарными словами
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public bool IsCanSplit(string text)
	{
		var cache = new HashSet<int>();
		var queue = new Queue<int>();
		queue.Enqueue(0);

		while (queue.Count > 0)
		{
			var startIndex = queue.Dequeue();

			if (cache.Contains(startIndex))
			{
				continue;
			}

			cache.Add(startIndex);

			var currentNode = _root;
			for (var i = startIndex; i < text.Length; i++)
			{
				var letter = text[i];
				
				if (currentNode.Edges.TryGetValue(letter, out var node) == false)
				{
					break;
				}

				if (i == text.Length - 1 && node.IsTerminal)
				{
					return true;
				}

				if (node.IsTerminal)
				{
					queue.Enqueue(i + 1);
				}

				currentNode = node;
			}
		}

		return false;
	}

	/// <summary>
	/// Узел хранит и добавляет рёбра
	/// </summary>
	class Node
	{
		private bool _isTerminal = false;
		private readonly Dictionary<char, Node> _edges = new();
		public bool IsTerminal => _isTerminal;
		public IReadOnlyDictionary<char, Node> Edges => _edges;

		public Node Add(char letter)
		{
			return _edges[letter] = new Node();
		}

		public void MarkTerminal()
		{
			_isTerminal = true;
		}
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var text = Contest.GetValue();
		var n = Contest.GetValue(int.Parse);
		var trie = new Trie();
		for (var i = 0; i < n; i++)
		{
			var pattern = Contest.GetValue();
			trie.Add(pattern);
		}

		(trie.IsCanSplit(text) ? "YES" : "NO").Print();
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