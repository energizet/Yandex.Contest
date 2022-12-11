using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		Contest.GetValue();
		var arr = Contest.GetList();

		var res = GetResult(arr);

		Console.WriteLine(res);
	}

	static int GetResult(List<string> arr)
	{
		var maxRounds = new List<int>();
		for (int i = 0; i < arr.Count; i++)
		{
			maxRounds.Add(GetMaxRounds(arr, i));
		}

		return maxRounds.DefaultIfEmpty().Max();
	}

	static int GetMaxRounds(List<string> arr, int start)
	{
		var zero = 0;
		var one = 0;
		var max = 0;
		for (int i = start; i < arr.Count; i++)
		{
			if (arr[i] == "0")
			{
				zero++;
			}

			if (arr[i] == "1")
			{
				one++;
			}

			if (zero == one)
			{
				max = zero + one;
			}
		}

		return max;
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