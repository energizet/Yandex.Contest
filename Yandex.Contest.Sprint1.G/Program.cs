using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
	static void Main(string[] args)
	{
		var num = GetValue(int.Parse);

		var res = GetResult(num);
		Console.WriteLine(res);
	}

	static string GetResult(int num)
	{
		var nums = new List<int>();

		do
		{
			var n = num % 2;
			nums.Add(n);
			num /= 2;
		} while (num > 0);

		nums.Reverse();
		return string.Join("", nums);
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
