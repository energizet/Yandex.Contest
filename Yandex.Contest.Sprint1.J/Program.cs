using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var num = GetValue(int.Parse);

		var res = GetResult(num);
		Console.WriteLine(string.Join(" ", res));
	}

	static List<int> GetResult(int num)
	{
		var nums = new List<int>();

		while (num % 2 == 0)
		{
			nums.Add(2);
			num /= 2;
		}

		for (var i = 3; num > 1;)
		{
			if (num % i == 0)
			{
				nums.Add(i);
				num /= i;
				continue;
			}

			i += 2;
		}

		return nums;
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

	static List<string> GetList<T>(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.ToList();
	}
}
