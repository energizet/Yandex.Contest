using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal static class Program
{
	static void Main(string[] args)
	{
		var str1 = Contest.GetValue();
		var str2 = Contest.GetValue();

		(IsEqual(str1, str2) ? "OK" : "FAIL").Print();
	}

	static bool IsEqual(string str1, string str2)
	{
		var lowStr = str1.Length < str2.Length ? str1 : str2;
		var highStr = str1.Length >= str2.Length ? str1 : str2;

		if (highStr.Length - lowStr.Length > 1)
		{
			return false;
		}

		if (lowStr.Length == highStr.Length)
		{
			var diffCount = 0;
			for (var i = 0; i < lowStr.Length; i++)
			{
				if (lowStr[i] == highStr[i])
				{
					continue;
				}

				diffCount++;
				if (diffCount > 1)
				{
					break;
				}
			}

			return diffCount <= 1;
		}

		var shift = 0;
		for (var i = 0; i < lowStr.Length; i++)
		{
			if (lowStr[i] == highStr[shift + i])
			{
				continue;
			}

			if (shift > 1)
			{
				return false;
			}

			if (lowStr[i] == highStr[shift + i + 1])
			{
				shift++;
				continue;
			}

			return false;
		}

		return true;
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