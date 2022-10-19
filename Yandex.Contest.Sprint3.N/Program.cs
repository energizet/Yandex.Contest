using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var n = GetValue(int.Parse);
		var list = new List<(int, int)>();
		for (int i = 0; i < n; i++)
		{
			var arr = GetArray(int.Parse);
			list.Add((arr[0], arr[1]));
		}

		var res = Calculate(list.ToArray());

		Console.WriteLine(string.Join("\n", res.Select(item => $"{item.start} {item.end}")));
	}

	static (int start, int end)[] Calculate((int start, int end)[] arr)
	{
		var sorted = arr.OrderBy(item => item.start);

		var stack = new Stack<(int start, int end)>();
		foreach (var item in sorted)
		{
			if (stack.Count <= 0)
			{
				stack.Push(item);
				continue;
			}

			var last = stack.Pop();

			if (item.start > last.end)
			{
				stack.Push(last);
				stack.Push(item);
				continue;
			}

			if (item.end <= last.end)
			{
				stack.Push(last);
				continue;
			}

			stack.Push((last.start, item.end));
		}

		return stack.Reverse().ToArray();
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