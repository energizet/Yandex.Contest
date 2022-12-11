using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

internal class Program
{
	private static List<string> _prefixes;

	static void Main(string[] args)
	{
		var q = 1000;
		var r = 123_987_123;

		var dic = new Dictionary<int, string>();
		_prefixes = new() {""};
		for (int i = 1;; i++)
		{
			foreach (var str in GenerateString())
			{
				var hash = Hash(str, q, r);
				if (dic.ContainsKey(hash))
				{
					Console.WriteLine(str);
					Console.WriteLine(dic[hash]);
					return;
				}

				dic[hash] = str;
			}
		}
	}

	static IEnumerable<string> GenerateString()
	{
		var prefixes = new List<string>();
		foreach (var str in _prefixes)
		{
			for (var c = 'a'; c <= 'z'; c++)
			{
				var newStr = str + c;
				prefixes.Add(newStr);
				yield return newStr;
			}
		}

		_prefixes = prefixes;
	}

	static int Hash(string s, int q, int r)
	{
		return (int) s.Aggregate(0.0, (current, t) => (current * q + t) % r);
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