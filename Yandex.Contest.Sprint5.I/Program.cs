using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		//Contest.GetValue(int.Parse).ToString().Print();
		Console.WriteLine($"{Count(2)} == 2");
		Console.WriteLine($"{Count(3)} == 5");
		Console.WriteLine($"{Count(4)} == 14");
		Console.WriteLine($"{Count(5)} == 42");
		Console.WriteLine($"{Count(6)} == 132");
		Console.WriteLine($"{Count(7)} == 429");
		Console.WriteLine($"{Count(8)} == 1430");
		Console.WriteLine($"{Count(9)} == 4862");
		Console.WriteLine($"{Count(10)} == 16796");
	}

	static int Count(int n)
	{
		if (n == 0)
		{
			return 0;
		}

		if (n == 1)
		{
			return 1;
		}

		n--;
		var count = 0;
		for (int i = 0; i < n; i++)
		{
			var temp = Count(n - i) + Count(i);
			if (n - i != i)
			{
				temp *= 2;
			}

			count += temp;
		}

		return count;
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