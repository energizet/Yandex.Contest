using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal static class Program
{
	static void Main(string[] args)
	{
		var str = Contest.GetValue();
		var oldValue = Contest.GetValue();
		var newValue = Contest.GetValue();
		str.GlobalReplace(oldValue, newValue).Print();
	}

	static string GlobalReplace(this string str, string oldValue, string newValue)
	{
		var replacing = $"{oldValue}#{str}".GetPrefixes()
			.Select((item, index) => new {item, index})
			.Where(item => item.item == oldValue.Length)
			.Select(item => item.index)
			.Select(item => item - oldValue.Length * 2);
		var queue = new Queue<int>(replacing);
		if (queue.Any() == false)
		{
			return str;
		}

		var newStr = new char[str.Length + queue.Count * (newValue.Length - oldValue.Length)];

		var replaceIndex = queue.Dequeue();
		var shift = 0;
		for (var i = 0; i < str.Length; i++)
		{
			if (i != replaceIndex)
			{
				newStr[i + shift] = str[i];
				continue;
			}

			for (var j = 0; j < newValue.Length; j++)
			{
				newStr[i + shift + j] = newValue[j];
			}

			i += oldValue.Length - 1;
			shift += newValue.Length - oldValue.Length;
			if (queue.Count > 0)
			{
				replaceIndex = queue.Dequeue();
			}
		}

		return newStr.Join("");
	}

	static IEnumerable<int> GetPrefixes(this string str)
	{
		var arr = new int[str.Length];
		for (var i = 1; i < str.Length; i++)
		{
			var k = arr[i - 1];

			while (k > 0 && str[i] != str[k])
			{
				k = arr[k - 1];
			}

			if (str[i] == str[k])
			{
				arr[i] = k + 1;
				continue;
			}

			arr[i] = 0;
		}

		return arr;
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