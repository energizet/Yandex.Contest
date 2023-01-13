using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// https://contest.yandex.ru/contest/26133/run-report/80705985/
/// 
/// -- ПРИНЦИП РАБОТЫ --
///
/// Распаковываем упакованную строку выполнена в виде генератора для оптимизации
/// 
/// Если найдена цифра - она записывается в буфер для составления числа больше 9 (хотя в контесте таких нет)
/// При появлении [ - парсится число из буфера и буфер обнуляется
/// и запускается рекурсия со следующего символа
/// Из вложеного генератора сохраняем символы в список и выбрасываем его дальше
/// Далее запускается повторная выдача из сохранёного списка
/// Поле завершения переносим указатель на ] для продолжения внешней распаковки
/// А на самом ] - сохраняем в lastIndex последний обработаный индекс запакованой строки
/// lastIndex изменяется перед непосредственным завершение генератора
/// 
/// Добавляем символы из генератора в бор
/// Поскольку нам нужно хранить только общий префикс - добавляем узлы пока не появится развитвление
///
/// Первую строку придётся распаковать и сохранить в бор полностью
/// 
/// Получаем все символы префикса до развитвления
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(L) - L - длина всех строк
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(L) - L - длина всех строк
/// 
/// </summary>
/// <summary>
/// Префикстное дерево
/// </summary>
internal class Trie
{
	private readonly Node _root = new();

	/// <summary>
	/// Вставляет рёбра в дерево пока не дойдёт до развитвления
	///
	/// Время - O(n)
	/// Память - O(n)
	/// </summary>
	/// <param name="word"></param>
	public void Add(IEnumerable<char> word)
	{
		var currentNode = _root;
		foreach (var letter in word)
		{
			if (currentNode.Edges.ContainsKey(letter))
			{
				currentNode = currentNode.Edges[letter];
				continue;
			}

			if (currentNode.Edges.Count > 0)
			{
				currentNode.Add(letter);
				return;
			}

			currentNode = currentNode.Add(letter);
		}
	}

	/// <summary>
	/// Ищем первый символ после которого начинается развитвление
	///
	/// Время - O(n)
	/// Память - O(n)
	/// </summary>
	/// <returns></returns>
	public string GetGlobalPrefix()
	{
		var builder = new StringBuilder();
		var currentNode = _root;
		while (currentNode.Edges.Count == 1)
		{
			var edge = currentNode.Edges.First();
			builder.Append(edge.Key);
			currentNode = edge.Value;
		}

		return builder.ToString();
	}

	/// <summary>
	/// Узел хранит и добавляет рёбра
	/// </summary>
	class Node
	{
		private readonly Dictionary<char, Node> _edges = new();
		public IReadOnlyDictionary<char, Node> Edges => _edges;

		public Node Add(char letter)
		{
			return _edges[letter] = new Node();
		}
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var n = Contest.GetValue(int.Parse);
		var trie = new Trie();
		for (var i = 0; i < n; i++)
		{
			var packedStr = Contest.GetValue();
			var unpackedStr = packedStr.UnpackString();
			trie.Add(unpackedStr);
		}

		trie.GetGlobalPrefix().Print();
	}

	/// <summary>
	/// Обёртка для распаковки всей строки
	/// </summary>
	/// <param name="packed"></param>
	/// <returns></returns>
	static IEnumerable<char> UnpackString(this string packed)
	{
		return UnpackString(packed, 0, new LastIndex());
	}

	/// <summary>
	/// Распаковка строки/подстроки
	///
	/// Время - O(n)
	/// Память - O(n)
	/// </summary>
	/// <param name="packed"></param>
	/// <param name="start"></param>
	/// <param name="lastIndex"></param>
	/// <returns></returns>
	static IEnumerable<char> UnpackString(string packed, int start, LastIndex lastIndex)
	{
		var numberBuilder = new StringBuilder();
		for (var i = start; i < packed.Length; i++)
		{
			if (char.IsLetter(packed[i]))
			{
				yield return packed[i];
				continue;
			}

			if (char.IsNumber(packed[i]))
			{
				numberBuilder.Append(packed[i]);
				continue;
			}

			if (packed[i] == '[')
			{
				var multiplier = int.Parse(numberBuilder.ToString());
				numberBuilder.Clear();

				var unpackedSubStr = UnpackString(packed, i + 1, lastIndex);

				var unpackedList = new List<char>();
				foreach (var c in unpackedSubStr)
				{
					unpackedList.Add(c);
					yield return c;
				}

				i = lastIndex.Index;

				for (var j = 1; j < multiplier; j++)
				{
					foreach (var c in unpackedList)
					{
						yield return c;
					}
				}

				continue;
			}

			if (packed[i] == ']')
			{
				lastIndex.Index = i;
				yield break;
			}
		}

		lastIndex.Index = packed.Length - 1;
	}

	/// <summary>
	/// Хранит последний индекс при распаковке строки
	/// </summary>
	private class LastIndex
	{
		public int Index { get; set; }
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