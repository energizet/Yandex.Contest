using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var whatFind = Console.ReadLine()!.ToCharArray();
		var whereFind = Console.ReadLine()!.ToCharArray();

		Console.WriteLine(Calculate(whatFind, whereFind));
	}

	static bool Calculate(char[] whatFind, char[] whereFind)
	{
		int i1 = 0, i2 = 0;
		while (i1 < whatFind.Length && i2 < whereFind.Length)
		{
			if (whatFind[i1] == whereFind[i2])
			{
				i1++;
			}

			i2++;
		}

		return i1 == whatFind.Length;
	}

	static T GetValue<T>(Func<string, T> func)
	{
		return func(Console.ReadLine()!);
	}

	static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()!
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func)
			.ToList();
	}

	static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()!
			.Split(new[] {' ', '\t'}, splitOptions)
			.ToList();
	}

	static T[] GetArray<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(func, splitOptions).ToArray();
	}

	static string[] GetArray(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(splitOptions).ToArray();
	}
}