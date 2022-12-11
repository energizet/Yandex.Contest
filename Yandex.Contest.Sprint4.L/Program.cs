using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		var list = Contest.GetList(int.Parse);
		var n = list[0];
		var k = list[1];
		var str = Contest.GetValue();

		var dic = new Dictionary<int, int[]>();
		for (int i = 0; i < str.Length - n + 1; i++)
		{
			var hash = Hash(str, 1_000_000_007, Int32.MaxValue / 2, i, n);
			if (!dic.ContainsKey(hash))
			{
				dic[hash] = new[] {i, 0};
			}

			dic[hash][1]++;
		}

		string.Join(" ", dic
				.Where(item => item.Value[1] >= k)
				.Select(item => item.Value[0])
			)
			.Print();
	}

	static int Hash(string s, int q, int r, int start, int count)
	{
		var result = 0;
		for (var i = 0; i < count; i++)
		{
			result = (result * q % r + s[i + start]) % r;
		}

		return result % r;
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