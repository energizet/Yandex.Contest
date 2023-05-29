using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var count = Contest.GetValue(int.Parse);
		var stack = new Stack();

		for (var i = 0; i < count; i++)
		{
			var command = Contest.GetList();
			Do(stack, command);
		}
	}

	static void Do(Stack stack, List<string> command)
	{
		switch (command[0])
		{
			case "push":
				stack.Push(int.Parse(command[1]));
				break;
			case "pop":
				stack.Pop();
				break;
			case "get_max":
				stack.GetMax();
				break;
		}
	}
}

class Stack
{
	private List<(int value, int max)> _stack = new();
	private int _count = 0;

	public void Push(int item)
	{
		(int, int) insertItem;

		if (_count == 0)
		{
			insertItem = (item, item);
		}
		else
		{
			insertItem = (item, Math.Max(item, _stack[_count - 1].max));
		}

		if (_count < _stack.Count)
		{
			_stack[_count] = insertItem;
		}
		else
		{
			_stack.Add(insertItem);
		}

		_count++;
	}

	public void Pop()
	{
		if (_count <= 0)
		{
			"error".Print();
			return;
		}

		_count--;
	}

	public void GetMax()
	{
		if (_count <= 0)
		{
			"None".Print();
			return;
		}

		_stack[_count - 1].max.Print();
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