using System;
using System.Collections.Generic;
using System.Linq;

class StackNode
{
	public int Value { get; set; }
	public int Max { get; set; }
}

class StackMaxEffective
{
	private LinkedList<StackNode> stack;

	public StackMaxEffective()
	{
		stack = new LinkedList<StackNode>();
	}

	public void Push(int item)
	{
		var last = stack.Last?.Value.Max;
		var node = new StackNode
		{
			Value = item,
			Max = !last.HasValue ? item : Math.Max(last.Value, item)
		};

		stack.AddLast(node);
	}

	public void Pop()
	{
		if (stack.Count <= 0)
		{
			throw new Exception("error");
		}

		stack.RemoveLast();
	}

	public int GetMax()
	{
		if (stack.Count <= 0)
		{
			throw new Exception("None");
		}

		return stack.Last.Value.Max;
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var count = GetValue(int.Parse);
		var stack = new StackMaxEffective();

		for (int i = 0; i < count; i++)
		{
			var command = GetList();
			Do(stack, command);
		}
	}

	static void Do(StackMaxEffective stack, List<string> command)
	{
		try
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
					Console.WriteLine(stack.GetMax());
					break;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
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
