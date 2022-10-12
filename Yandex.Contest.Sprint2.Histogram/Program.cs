using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var arr = GetList(int.Parse);
		var res = GetResult(arr);

		Console.WriteLine(res);
	}

	static int GetResult(List<int> arr)
	{
		var lefts = GetLefts(arr);
		var rights = GetRights(arr);

		return arr.Select((item, i) => item * (rights[i] - lefts[i] - 1)).Max();
	}

	static List<int> GetLefts(List<int> arr)
	{
		var stack = new Stack<int>();
		var lefts = new List<int>();

		for (int i = 0; i < arr.Count; i++)
		{
			var item = arr[i];

			while (true)
			{
				if (!stack.TryPop(out var pop))
				{
					stack.Push(i);
					lefts.Add(-1);
					break;
				}

				var left = arr[pop];
				if (left >= item)
				{
					continue;
				}

				stack.Push(pop);
				stack.Push(i);
				lefts.Add(pop);
				break;
			}
		}

		return lefts;
	}

	static List<int> GetRights(List<int> arr)
	{
		var stack = new Stack<int>();
		var rights = new List<int>();

		for (int i = arr.Count - 1; i >= 0; i--)
		{
			var item = arr[i];

			while (true)
			{
				if (!stack.TryPop(out var pop))
				{
					stack.Push(i);
					rights.Add(arr.Count);
					break;
				}

				var right = arr[pop];
				if (right >= item)
				{
					continue;
				}

				stack.Push(pop);
				stack.Push(i);
				rights.Add(pop);
				break;
			}
		}

		rights.Reverse();
		return rights;
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
