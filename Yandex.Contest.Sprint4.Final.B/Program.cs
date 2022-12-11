using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// https://contest.yandex.ru/contest/24414/run-report/73000625/
/// 
/// -- ПРИНЦИП РАБОТЫ --
///
/// В задании сказано, что не требуется рехеширование и максимальный размер должен быть меньше 10^5
/// поэтому я взял близжайшее максимальное простое число 99971
///
/// GetHashMapIndex - получает номер корзины по ключу, берёт остаток от размера HashMap
/// 
/// Put
/// - получает индекс корзины
/// - ищет ключ, если нашлось, просто перезаписать значение и выйти
/// - добавляем в первую позицию связного списка, если нужно добавим указатель на новый элемент в существующий
///
/// Get
/// - возвращаю bool, потому что нужно передавать ошибку, а с try...catch уходит в TimeLimit даже с Dictionary
/// https://contest.yandex.ru/contest/24414/run-report/72998770/
/// - получает индекс корзины
/// - проходимся по свюзному списку пока не найдём нужный ключ
///
/// Delete
/// - bool, аналогично Get
/// - получает индекс корзины
/// - проходимся по свюзному списку пока не найдём нужный ключ
/// - у следующего элемента указываем Prev на текущий Prev, у предыдущего Next на текущий Next
/// - если необходимо меняем голову
/// 
/// -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
///
/// Прохождение тестов и скорость аналогичная Dictionary
/// https://files.slack.com/files-pri/TPV9DP0N4-F047TBLB09L/image.png
/// https://contest.yandex.ru/contest/24414/run-report/72999982/
///
/// А если серьёзно, вся магия HashMap в GetHashMapIndex,
/// поскольку число корзин равно простому числу и мы работаем с неотрицательными числами
/// то остаток от деления будет всегда возвращать значение в пределах индексов массива
///
/// Всё остальное это просто работа со связным списком
///
/// Двух связный список выбран для унификации поиска элемента при всех опирациях
/// В односвязном списке операция удаления отличалась бы от поиска
/// Выбран он для повышения читаемости, в ушерб потребления памяти
/// 
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// Put - O(1)
/// Get - O(1 + a), где a = N / M, где N число элементов, а M количество корзин
/// Delete - O(1 + a)
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(M * a) -> O(M * (N / M)) -> O(N) - на хранения элементов связного списка, где n число элементов
/// 
/// </summary>
class MyHashMapNode
{
	public int Key { get; set; }
	public int Value { get; set; }
	public MyHashMapNode? Prev { get; set; }
	public MyHashMapNode? Next { get; set; }
}

class MyHashMap
{
	private const int PrimeHashMapSize = 99971;
	private readonly MyHashMapNode?[] _hashMap = new MyHashMapNode[PrimeHashMapSize];

	/// <summary>
	/// - получает индекс корзины
	/// - ищет ключ, если нашлось, просто перезаписать значение и выйти
	/// - добавляем в первую позицию связного списка, если нужно добавим указатель на новый элемент в существующий
	/// </summary>
	/// <param name="key">ключ</param>
	/// <param name="value">значение</param>
	public void Put(int key, int value)
	{
		var index = GetHashMapIndex(key);
		var node = FindNode(_hashMap[index], key);
		if (node != null)
		{
			node.Value = value;
			return;
		}

		node = new MyHashMapNode
		{
			Key = key,
			Value = value,
			Next = _hashMap[index],
		};

		if (_hashMap[index] != null)
		{
			_hashMap[index]!.Prev = node;
		}

		_hashMap[index] = node;
	}

	/// <summary>
	/// - возвращаю bool, потому что нужно передавать ошибку, а с try...catch уходит в TimeLimit даже с Dictionary
	/// https://contest.yandex.ru/contest/24414/run-report/72998770/
	/// - получает индекс корзины
	/// - проходимся по свюзному списку пока не найдём нужный ключ
	/// </summary>
	/// <param name="key">ключ</param>
	/// <param name="res">результат</param>
	/// <returns>true - если результат получен</returns>
	public bool TryGet(int key, out int res)
	{
		var index = GetHashMapIndex(key);
		var node = FindNode(_hashMap[index], key);
		if (node == null)
		{
			res = 0;
			return false;
		}

		res = node.Value;
		return true;
	}

	/// <summary>
	/// - bool, аналогично Get
	/// - получает индекс корзины
	/// - проходимся по свюзному списку пока не найдём нужный ключ
	/// - у следующего элемента указываем Prev на текущий Prev, у предыдущего Next на текущий Next
	/// - если необходимо меняем голову
	/// </summary>
	/// <param name="key">ключ</param>
	/// <param name="res">результат</param>
	/// <returns>true - если результат получен</returns>
	public bool TryDelete(int key, out int res)
	{
		var index = GetHashMapIndex(key);
		var node = FindNode(_hashMap[index], key);
		if (node == null)
		{
			res = 0;
			return false;
		}

		var value = node.Value;

		if (node.Next != null)
		{
			node.Next.Prev = node.Prev;
		}

		if (node.Prev != null)
		{
			node.Prev.Next = node.Next;
		}
		else
		{
			_hashMap[index] = node.Next;
		}

		res = value;
		return true;
	}

	/// <summary>
	/// получает номер корзины по ключу, берёт остаток от размера HashMap
	/// </summary>
	/// <param name="key">ключ</param>
	/// <returns>индекс корзины</returns>
	private int GetHashMapIndex(int key)
	{
		return key % _hashMap.Length;
	}

	/// <summary>
	/// Поиск элемента с подходящим ключом в связном списке
	/// </summary>
	/// <param name="node">стартовый элемент</param>
	/// <param name="key">ключ</param>
	/// <returns>элемент с ключом или null</returns>
	private MyHashMapNode? FindNode(MyHashMapNode? node, int key)
	{
		if (node == null)
		{
			return null;
		}

		if (node.Key == key)
		{
			return node;
		}

		return FindNode(node.Next, key);
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var hashMap = new MyHashMap();
		var n = Contest.GetValue(int.Parse);

		var stringBuilder = new StringBuilder();
		for (int i = 0; i < n; i++)
		{
			ExecuteCommand(hashMap, Contest.GetList(), stringBuilder);
		}

		stringBuilder.ToString().Print();
	}

	static void ExecuteCommand(MyHashMap hashMap, List<string> command, StringBuilder stringBuilder)
	{
		int res;
		switch (command[0])
		{
			case "put":
				hashMap.Put(int.Parse(command[1]), int.Parse(command[2]));
				return;
			case "get":
				if (!hashMap.TryGet(int.Parse(command[1]), out res))
				{
					stringBuilder.Append("None\n");
					return;
				}

				stringBuilder.Append(res);
				stringBuilder.Append('\n');
				return;
			case "delete":
				if (!hashMap.TryDelete(int.Parse(command[1]), out res))
				{
					stringBuilder.Append("None\n");
					return;
				}

				stringBuilder.Append(res);
				stringBuilder.Append('\n');
				return;
		}
	}
}

static class Contest
{
	public static string GetValue()
	{
		return Console.ReadLine()!;
	}

	public static T GetValue<T>(Func<string, T> func)
	{
		return func(GetValue());
	}

	public static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func)
			.ToList();
	}

	public static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.ToList();
	}

	public static T[] GetArray<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(func, splitOptions).ToArray();
	}

	public static string[] GetArray(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(splitOptions).ToArray();
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join().Print();
	}
}