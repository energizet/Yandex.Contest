using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

internal static class Program
{
	static void Main(string[] args)
	{
		var n = Contest.GetValue(int.Parse);

		int? last = null;
		for (var i = 0; i < n; i++)
		{
			var item = Contest.GetValue(int.Parse);
			if (item == last)
			{
				continue;
			}

			last = item;
			item.Print();
			if (i % 100 == 0)
			{
				GC.Collect();
			}
		}
	}
}

/// <summary>
/// I/O методы
/// </summary>
internal static class Contest
{
	private static TextReader s_reader;
	private static TextWriter s_writer;

	static Contest()
	{
		s_reader = new StreamReader(File.Open("input.txt", FileMode.Open));
		s_writer = new StreamWriter(File.Open("output.txt", FileMode.Create));
	}

	public static string GetValue()
	{
		return s_reader.ReadLine()!;
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
		s_writer.WriteLine(value);
		s_writer.Flush();
	}

	public static void Print(this object obj)
	{
		s_writer.WriteLine(obj);
		s_writer.Flush();
	}

	public static void Print(this string str)
	{
		s_writer.WriteLine(str);
		s_writer.Flush();
	}
}