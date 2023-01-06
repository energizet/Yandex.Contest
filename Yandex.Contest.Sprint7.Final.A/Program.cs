using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// https://contest.yandex.ru/contest/25597/run-report/80172781/
///
/// -- ПРИНЦИП РАБОТЫ --
///
/// Формируем матрицу N + 1 на M + 1, где N длина s1 строки, M длина s2 строки
/// Заполняем первую строку [0, N] и первую колонку [0, M]
/// Проходим поочерёдно все строки
/// Рекурентная формула:
/// dp[i][j] = min(dp[i - 1][j - 1] + OneIfNotEqual, dp[i - 1][j] + 1, dp[i][j - 1] + 1)
/// где OneIfNotEqual - 0 если s1[i - 1] равер s2[j - 1], иначе 1
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
/// 
/// O(N * M) - где N длина s1 строки, M длина s2 строки
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(N * M) - где N длина s1 строки, M длина s2 строки
/// 
/// </summary>
internal static class Program
{
	static void Main(string[] args)
	{
		var str1 = Contest.GetValue();
		var str2 = Contest.GetValue();
		GetLevenshteynLength(str1, str2).Print();
	}

	/// <summary>
	/// Поиск растояния по Левеншнеину
	/// </summary>
	/// <param name="str1"></param>
	/// <param name="str2"></param>
	/// <returns></returns>
	static int GetLevenshteynLength(string str1, string str2)
	{
		var dp = new int[str2.Length + 1][];
		dp[0] = Enumerable.Range(0, str1.Length + 1).ToArray();

		for (var i = 1; i < str2.Length + 1; i++)
		{
			dp[i] = new int[str1.Length + 1];
			dp[i][0] = i;

			for (var j = 1; j < str1.Length + 1; j++)
			{
				var oneAdd = str1[j - 1] != str2[i - 1] ? 1 : 0;
				var minLength = Math.Min(dp[i - 1][j - 1] + oneAdd,
					Math.Min(dp[i - 1][j] + 1, dp[i][j - 1] + 1));

				dp[i][j] = minLength;
			}
		}

		return dp[^1][^1];
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