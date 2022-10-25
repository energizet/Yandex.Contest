using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://contest.yandex.ru/contest/23815/run-report/72618491/
/// 
/// Парсим строку пользователя в объект User, реализует IComparable
/// 
/// В User метод Compare сравнивает пользователей и выдаёт результат
/// в какой последовательности должны идти пользователи
/// В сортировке заводим указатеил на правый и левый элементы
/// 
/// Сравниваем оба элемента с ключевым
/// если левый меньше ключевого - переходим к следующему элементу
/// усли правый больше ключевого - переходим к предыдущему элементу
/// если левый больше и правый меньше ключевого - меняем их местами
/// 
/// Запускаем сортировку от левой элемента к найденной левой границе
///
/// Запускаем сортировку от левой границы к правому элементу
///
/// CPU среднее - O(n log n) - худшее - O(n^2)
/// Память - O(1)
/// </summary>
class User : IComparable<User>
{
	public string Name { get; set; }
	public int Score { get; set; }
	public int Fine { get; set; }

	public User(string user)
	{
		var arr = user.Split(" ");
		Name = arr[0];
		Score = int.Parse(arr[1]);
		Fine = int.Parse(arr[2]);
	}

	public int CompareTo(User? right)
	{
		if (right == null)
		{
			throw new ArgumentNullException("Compare object cannot be null");
		}

		if (Score != right.Score)
		{
			return -Score.CompareTo(right.Score);
		}

		if (Fine != right.Fine)
		{
			return Fine.CompareTo(right.Fine);
		}

		if (Name != right.Name)
		{
			return string.Compare(Name, right.Name, StringComparison.Ordinal);
		}

		return 0;
	}

	public override string ToString()
	{
		return $"{Name} {Score} {Fine}";
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var n = GetValue(int.Parse);
		var list = new List<User>();
		for (int i = 0; i < n; i++)
		{
			list.Add(new User(Console.ReadLine()!));
		}

		QuickSort(list, 0, list.Count);

		Console.WriteLine(string.Join("\n", list.Select(item => item.Name)));
	}

	static void QuickSort(List<User> users, int start, int end)
	{
		var left = start;
		var right = end - 1;

		if (right - left <= 0)
		{
			return;
		}

		var pivot = users[(start + end) / 2];
		while (right - left > 0)
		{
			var leftCompare = users[left].CompareTo(pivot) >= 0;
			var rightCompare = users[right].CompareTo(pivot) <= 0;
			if (!leftCompare && right - left > 0)
			{
				left++;
			}

			if (!rightCompare && right - left > 0)
			{
				right--;
			}

			if (leftCompare && rightCompare)
			{
				(users[left], users[right]) = (users[right], users[left]);
			}
		}

		QuickSort(users, start, left);
		QuickSort(users, left, end);
	}

	static T GetValue<T>(Func<string, T> func)
	{
		return func(Console.ReadLine()!);
	}

	static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()!
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func)
			.ToList();
	}

	static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()!
			.Split(new[] {' ', '\t'}, splitOptions)
			.ToList();
	}

	static T[] GetArray<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(func, splitOptions).ToArray();
	}

	static string[] GetArray(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(splitOptions).ToArray();
	}
}