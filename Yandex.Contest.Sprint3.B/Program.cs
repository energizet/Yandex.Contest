using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	private static Dictionary<int, char[]> keyboard = new Dictionary<int, char[]>
	{
		{ 2, new []{ 'a', 'b', 'c' } },
		{ 3, new []{ 'd', 'e', 'f' } },
		{ 4, new []{ 'g', 'h', 'i' } },
		{ 5, new []{ 'j', 'k', 'l' } },
		{ 6, new []{ 'm', 'n', 'o' } },
		{ 7, new []{ 'p', 'q', 'r', 's' } },
		{ 8, new []{ 't', 'u', 'v' } },
		{ 9, new []{ 'w', 'x', 'y', 'z' } },
	};

	static void Main(string[] args)
	{
		var nums = Console.ReadLine().Select(item => int.Parse(item.ToString())).ToArray();

		var res = new List<string>();
		Generate(nums, 0, "", res);

		Console.WriteLine(string.Join(" ", res));
	}

	static void Generate(int[] nums, int index, string prefix, List<string> results)
	{
		if (index >= nums.Length)
		{
			results.Add(prefix);
			return;
		}

		var litters = keyboard[nums[index]];

		foreach (var litter in litters)
		{
			Generate(nums, index + 1, prefix + litter, results);
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
