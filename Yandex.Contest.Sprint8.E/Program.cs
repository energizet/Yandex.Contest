using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

internal static class Program
{
	static void Main(string[] args)
	{
		var str = Contest.GetValue();
		var resLength = str.Length;
		var n = Contest.GetValue(int.Parse);
		var dic = new Dictionary<int, string>();
		for (var i = 0; i < n; i++)
		{
			var strI = Contest.GetArray();
			dic[int.Parse(strI[1])] = strI[0];
			resLength += strI[0].Length;
		}

		InsertInString(str, resLength, dic).Print();
	}

	static string InsertInString(string str, int resLength,
		IReadOnlyDictionary<int, string> inserts)
	{
		var resArr = new char[resLength];
		var shift = 0;
		for (var i = 0; i <= str.Length; i++)
		{
			if (inserts.ContainsKey(i))
			{
				var substr = inserts[i];
				for (var j = 0; j < substr.Length; j++)
				{
					resArr[shift + i + j] = substr[j];
				}

				shift += substr.Length;
			}

			if (i < str.Length)
			{
				resArr[shift + i] = str[i];
			}
		}

		return resArr.Join("");
	}
}

/// <summary>
/// I/O методы
/// </summary>
internal static class Contest
{
	public static string GetValue()
	{
		return Console.ReadLine()!;
	}

	public static T GetValue<T>(Func<string, T> func)
	{
		return func(GetValue());
	}

	public static IEnumerable<T> GetIEnumerable<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func);
	}

	public static List<T> GetList<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToList();
	}

	public static List<string> GetList(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(str => str, splitOptions);
	}

	public static T[] GetArray<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToArray();
	}

	public static string[] GetArray(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetArray(str => str, splitOptions);
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join(separator).Print();
	}

	public static void Print(this ValueType value)
	{
		Console.WriteLine(value);
	}

	public static void Print(this object obj)
	{
		Console.WriteLine(obj);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}
}