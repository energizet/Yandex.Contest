using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		var str = Contest.GetValue();

		var dir = new Dictionary<char, List<int>>();
		for (int i = 0; i < str.Length; i++)
		{
			if (!dir.ContainsKey(str[i]))
			{
				dir[str[i]] = new List<int>();
			}

			dir[str[i]].Add(i);
		}

		foreach (var item in dir)
		{
			for (var i = 1; i < item.Value.Count; i++)
			{
				var index = item.Value[i];
			}
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