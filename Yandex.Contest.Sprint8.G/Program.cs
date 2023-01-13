using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal static class Program
{
	static void Main(string[] args)
	{
		Contest.GetValue();
		var sequence = Contest.GetArray(int.Parse).ConvertToRelative().ToList();
		Contest.GetValue();
		var pattern = Contest.GetArray(int.Parse).ConvertToRelative().ToList();

		IndexOf(sequence, pattern).Print();
	}

	static IEnumerable<int> IndexOf(IReadOnlyList<int> sequence, IReadOnlyList<int> pattern)
	{
		var index = IndexOf(sequence, pattern, 0);
		while (index >= 0)
		{
			yield return ++index;
			index = IndexOf(sequence, pattern, index);
		}
	}

	static int IndexOf(IReadOnlyList<int> sequence, IReadOnlyList<int> pattern, int start)
	{
		for (var i = start; i < sequence.Count - pattern.Count + 1; i++)
		{
			var isEqual = IsEqual(sequence, pattern, i);
			if (isEqual)
			{
				return i;
			}
		}

		return -1;
	}

	static bool IsEqual(IReadOnlyList<int> sequence, IReadOnlyList<int> pattern, int shift)
	{
		for (var j = 0; j < pattern.Count; j++)
		{
			if (sequence[shift + j] != pattern[j])
			{
				return false;
			}
		}

		return true;
	}

	static IEnumerable<int> ConvertToRelative(this IReadOnlyList<int> arr)
	{
		for (var i = 1; i < arr.Count; i++)
		{
			yield return arr[i] - arr[i - 1];
		}
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