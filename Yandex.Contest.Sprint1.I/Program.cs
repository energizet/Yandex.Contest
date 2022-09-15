using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var num = GetValue(int.Parse);

		var res = GetResult(num);
		Console.WriteLine(res);
	}

	private static bool GetResult(int num)
	{
		var set = new HashSet<int> { 1, 4, 16, 64, 256, 1024, 4096, 8192, 16384, };

		return set.Contains(num);
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
