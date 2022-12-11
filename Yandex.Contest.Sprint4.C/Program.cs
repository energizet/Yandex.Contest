using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

internal class Program
{
	static void Main(string[] args)
	{
		var q = Contest.GetValue(long.Parse);
		var r = Contest.GetValue(long.Parse);
		var str = Contest.GetValue();

		var powers = GetPowers(q, str.Length, r).ToList();
		var hashes = GetHashes(str, q, r).ToList();

		var n = Contest.GetValue(int.Parse);
		for (int i = 0; i < n; i++)
		{
			var indexes = Contest.GetArray(int.Parse);
			var start = indexes[0] - 1;
			var end = indexes[1];

			var hash = (hashes[end] + r - (hashes[start] * powers[end - start]) % r) % r;
			hash.ToString().Print();
		}
	}

	private static IEnumerable<long> GetPowers(long num, long maxPow, long r)
	{
		var lastPow = 1L;
		for (int i = 0; i < maxPow; i++)
		{
			yield return lastPow;
			lastPow = lastPow * num % r;
		}

		yield return lastPow;
	}


	static IEnumerable<long> GetHashes(string s, long q, long r)
	{
		var result = 0L;
		foreach (var c in s)
		{
			yield return result;
			result = (result * q % r + c) % r;
		}

		yield return result;
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