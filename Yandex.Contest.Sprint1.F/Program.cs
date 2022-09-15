using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class Program
{
	static void Main(string[] args)
	{
		var str = Console.ReadLine();

		var res = GetResult(str);
		Console.WriteLine(res);
	}

	static bool GetResult(string str)
	{
		var regex = new Regex("[^a-zA-Z0-9]");
		str = regex.Replace(str, "").ToLower();

		for (int i = 0; i < str.Length / 2; i++)
		{
			if (str[i] != str[str.Length - 1 - i])
			{
				return false;
			}
		}

		return true;
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
