using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var count = GetValue(int.Parse);
		var stack = new List<int>();

		for (int i = 0; i < count; i++)
		{
			var command = GetList();
			Do(stack, command);
		}
	}

	static void Do(List<int> stack, List<string> command)
	{
		switch (command[0])
		{
			case "push":
				Push(stack, int.Parse(command[1]));
				break;
			case "pop":
				Pop(stack);
				break;
			case "get_max":
				GetMax(stack);
				break;
		}
	}

	static void Push(List<int> stack, int item)
	{
		stack.Add(item);
	}

	static void Pop(List<int> stack)
	{
		if (stack.Count <= 0)
		{
			Console.WriteLine("error");
			return;
		}

		stack.RemoveAt(stack.Count - 1);
	}

	static void GetMax(List<int> stack)
	{
		if (stack.Count <= 0)
		{
			Console.WriteLine("None");
			return;
		}

		Console.WriteLine(stack.Max());
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
