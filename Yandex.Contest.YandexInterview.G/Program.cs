using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

internal static class Program
{
	static void Main(string[] args)
	{
		var pointsIndexing = new List<(int, int)>();
		var points = new Dictionary<(int, int), int>();

		var n = Contest.GetValue(int.Parse);
		for (var i = 0; i < n; i++)
		{
			var pointArr = Contest.GetArray(int.Parse);
			var point = (pointArr[0], pointArr[1]);
			pointsIndexing.Add(point);
			points[point] = 0;
		}

		var len = Contest.GetValue(int.Parse);
		var fromTo = Contest.GetArray(int.Parse);
		var fromPoint = pointsIndexing[fromTo[0] - 1];
		var toPoint = pointsIndexing[fromTo[1] - 1];

		var stack = new Stack<(int, int)>();
		stack.Push(fromPoint);

		var lengthToPoint = 0;
		while (stack.Count > 0)
		{
			var tmp = new Stack<(int, int)>();
			while (stack.Count > 0)
			{
				var point = stack.Pop();
				if (point == toPoint)
				{
					lengthToPoint.Print();
					return;
				}

				points[point] = 1;
				Dfs(point.Item1, point.Item2, len, tmp, points);
			}

			lengthToPoint++;
			stack = tmp;
		}

		"-1".Print();
	}

	static void Dfs(int x, int y, int len, Stack<(int, int)> stack,
		Dictionary<(int, int), int> points)
	{
		if (len < 0)
		{
			return;
		}

		if (points.TryGetValue((x, y), out var point) && point == 0)
		{
			stack.Push((x, y));
		}

		Dfs(x + 1, y, len - 1, stack, points);
		Dfs(x - 1, y, len - 1, stack, points);
		Dfs(x, y + 1, len - 1, stack, points);
		Dfs(x, y - 1, len - 1, stack, points);
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