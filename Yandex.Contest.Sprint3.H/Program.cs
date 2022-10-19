using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var arr = GetList(int.Parse);

		arr.Sort((l, r) => Compare(l.ToString(), r.ToString()));
		Console.WriteLine(string.Join("", arr));
	}

	static int Compare(string l, string r)
	{
		return -(l + r).CompareTo(r + l);
	}

	static T GetValue<T>(Func<string, T> func)
	{
		return func(Console.ReadLine());
	}

	// 82 825 823 // 263 263 26
	// 825 823 82 // 26 263 263

	/*
	 
	1
	10 1

	1
	823 82

	1
	26 263

	*/

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
