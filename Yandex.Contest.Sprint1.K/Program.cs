using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var xArr = GetList();
		var k = GetValue(int.Parse);

		var res = GetResult(xArr, k);
		Console.WriteLine(string.Join(" ", res));
	}

	static List<string> GetResult(List<string> xArr, int k)
	{
		var x = int.Parse(string.Join("", xArr));

		return (x + k).ToString().Select(item => item.ToString()).ToList();
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
