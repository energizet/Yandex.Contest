using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		var first = Contest.GetValue();
		var second = Contest.GetValue();

		Console.WriteLine(IsEqual(first, second) && IsEqual(second, first) ? "YES" : "NO");
	}

	static bool IsEqual(string l, string r)
	{
		if (l.Length != r.Length)
		{
			return false;
		}

		var dic = new Dictionary<char, char>();

		for (int i = 0; i < l.Length; i++)
		{
			if (!dic.ContainsKey(l[i]))
			{
				dic[l[i]] = r[i];
				continue;
			}

			if (dic[l[i]] != r[i])
			{
				return false;
			}
		}

		return true;
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