using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var str = Console.ReadLine();

		var res = GetResult(str);
		Console.WriteLine(res);
	}

	private static bool GetResult(string str)
	{
		if (string.IsNullOrWhiteSpace(str))
		{
			return true;
		}

		var stack = new Stack<char>();
		var open = new Dictionary<char, char> {
			{ '{' , '}' },
			{ '[' , ']' },
			{ '(' , ')' },
		};
		var close = new HashSet<char> { '}', ']', ')' };

		foreach (var c in str)
		{
			if (open.ContainsKey(c))
			{
				stack.Push(open[c]);
			}

			if (!close.Contains(c))
			{
				continue;
			}

			if (stack.Count == 0)
			{
				return false;
			}

			var prev = stack.Pop();
			if (prev != c)
			{
				return false;
			}
		}

		return stack.Count == 0;
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
