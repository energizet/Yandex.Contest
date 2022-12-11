using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		var a = Contest.GetValue(int.Parse);
		var m = Contest.GetValue(int.Parse);
		var s = Contest.GetValue();

		Console.WriteLine(Hash(s, a, m));
	}

	static int Hash(string s, int q, int r)
	{
		return (int)s.Aggregate(0.0, (current, t) => (current * q + t) % r);
	}
}

static class Contest
{
	public static string GetValue()
	{
		return Console.ReadLine()!;
	}

	public static T GetValue<T>(Func<string, T> func)
	{
		return func(GetValue());
	}

	public static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func)
			.ToList();
	}

	public static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.ToList();
	}

	public static T[] GetArray<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(func, splitOptions).ToArray();
	}

	public static string[] GetArray(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(splitOptions).ToArray();
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join().Print();
	}
}