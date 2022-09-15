using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var homes = GetList(int.Parse);

		var res = GetResult(homes);
		Console.WriteLine(string.Join(" ", res));
	}

	static List<int> GetResult(List<int> homes)
	{
		var empties = new List<int>();
		for (int i = 0; i < homes.Count; i++)
		{
			var home = homes[i];
			if (home == 0)
			{
				empties.Add(i);
			}
		}

		var res = homes.Select((_, i) => GetMinClose(i, empties)).ToList();
		return res;
	}

	static int GetMinClose(int current, List<int> empties)
	{
		var (li, ri) = GetBorder(current, empties);
		var (l, r) = (empties[li], empties[ri]);
		var (dl, dr) = (Math.Abs(current - l), Math.Abs(current - r));

		var min = Math.Min(dl, dr);
		return min;
	}

	static (int l, int r) GetBorder(int current, List<int> empties)
	{
		var left = 0;
		var right = empties.Count - 1;

		while (right - left > 1)
		{
			var middle = (right + left) / 2;
			var item = empties[middle];

			if (current == item)
			{
				return (middle, middle);
			}

			if (current < item)
			{
				right = middle;
				continue;
			}

			if (current > item)
			{
				left = middle;
				continue;
			}
		}

		return (left, right);
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
