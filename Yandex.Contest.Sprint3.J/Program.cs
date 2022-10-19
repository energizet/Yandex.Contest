using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var arr = GetList(int.Parse);
		BobbleSort(arr);
	}

	static void BobbleSort(List<int> arr)
	{
		var isSorted = true;
		for (int i = 0; i < arr.Count; i++)
		{
			var isSwaped = false;
			for (int j = 1; j < arr.Count - i; j++)
			{
				if (arr[j - 1] > arr[j])
				{
					isSorted = false;
					isSwaped = true;
					(arr[j - 1], arr[j]) = (arr[j], arr[j - 1]);
				}
			}

			if (isSwaped)
			{
				Console.WriteLine(string.Join(" ", arr));
			}
		}

		if (isSorted)
		{
			Console.WriteLine(string.Join(" ", arr));
		}
	}

	static T GetValue<T>(Func<string, T> func)
	{
		return func(Console.ReadLine());
	}

	static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.Select(func)
				.ToList();
	}

	static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.ToList();
	}
}
