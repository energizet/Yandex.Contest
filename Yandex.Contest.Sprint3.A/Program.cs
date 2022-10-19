using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var n = GetValue(int.Parse);

		var res = new List<string>();
		Generate(n * 2, 0, "", res);

		Console.WriteLine(string.Join("\n", res));
	}

	static void Generate(int n, int open, string prefix, List<string> results)
	{
		if (n == 0)
		{
			results.Add(prefix);
			return;
		}

		if (open < n)
		{
			Generate(n - 1, open + 1, prefix + "(", results);
		}

		if (open > 0)
		{
			Generate(n - 1, open - 1, prefix + ")", results);
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
