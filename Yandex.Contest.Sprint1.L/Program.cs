using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var etalon = Console.ReadLine();
		var test = Console.ReadLine();

		var res = GetResult(etalon, test);
		Console.WriteLine(res);
	}

	static char GetResult(string etalon, string test)
	{
		var etalonDic = new Dictionary<char, int>();
		foreach (char c in etalon)
		{
			if (!etalonDic.ContainsKey(c))
			{
				etalonDic.Add(c, 0);
			}

			etalonDic[c]++;
		}

		foreach (var c in test)
		{
			if (!etalonDic.ContainsKey(c))
			{
				return c;
			}

			if (etalonDic[c] == 0)
			{
				return c;
			}

			etalonDic[c]--;
		}

		throw new InvalidOperationException("Something wrong");
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
