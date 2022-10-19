using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var arr = GetArray(int.Parse);

		CountingSort(arr);

		Console.WriteLine(string.Join(" ", arr));
	}

	static void CountingSort(int[] arr)
	{
		var counting = new int[3];
		foreach (var item in arr)
		{
			counting[item]++;
		}

		var index = 0;
		for (var i = 0; i < counting.Length; i++)
		{
			for (int j = 0; j < counting[i]; j++)
			{
				arr[index++] = i;
			}
		}
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