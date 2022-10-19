using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var n = GetValue(int.Parse);
		var days = GetList(int.Parse);
		var price = GetValue(int.Parse);

		var oneBike = FindFirstDay(days, price, 0, days.Count - 1);
		var twoBike = FindFirstDay(days, price * 2, 0, days.Count - 1);
		Console.WriteLine($"{oneBike} {twoBike}");
	}

	static int FindFirstDay(List<int> arr, int price, int left, int right)
	{
		if (arr[right] < price)
		{
			return -1;
		}

		if (arr[left] >= price)
		{
			return left + 1;
		}

		if (right - left <= 1)
		{
			if (arr[right] >= price)
			{
				return right + 1;
			}

			return -1;
		}

		var middle = (left + right) / 2;

		if (arr[middle] >= price)
		{
			return FindFirstDay(arr, price, left, middle);
		}

		return FindFirstDay(arr, price, middle, right);
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
