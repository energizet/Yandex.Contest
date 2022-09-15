using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var max = GetValue(int.Parse) * 2;
		var field = new List<List<int>>
		{
			GetList(GetItem),
			GetList(GetItem),
			GetList(GetItem),
			GetList(GetItem),
		};

		var res = GetResult(max, field);
		Console.WriteLine(res);
	}

	static int GetResult(int max, List<List<int>> field)
	{
		var number = field.SelectMany(x => x)
			.Where(x => x > 0)
			.GroupBy(item => item, (key, group) => group.Count())
			.Select(item => item <= max ? 1 : 0)
			.Sum();

		return number;
	}

	static int GetItem(char c)
	{
		var isNum = int.TryParse(c.ToString(), out var num);
		if (isNum)
		{
			return num;
		}

		return -1;
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

	static List<T> GetList<T>(Func<char, T> func)
	{
		return Console.ReadLine()
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
