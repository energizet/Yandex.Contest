using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		var arr = new[] { 1, 9, 4, 6, 8, 2, 3, 5, 7 };
		Console.WriteLine(string.Join(",", arr));

		var sorted = MergeSort(arr);
		var sorted2 = MergeSort(arr[..^1], arr[^1]);

		Console.WriteLine(string.Join(",", sorted));
		Console.WriteLine(string.Join(",", sorted2));
	}

	static int[] MergeSort(int[] arr)
	{
		if (arr.Length <= 1)
		{
			return arr;
		}

		var left = MergeSort(arr[..(arr.Length / 2)]);
		var right = MergeSort(arr[(arr.Length / 2)..]);

		var l = 0;
		var r = 0;
		var k = 0;
		while (l < left.Length && r < right.Length)
		{
			if (left[l] <= right[r])
			{
				arr[k++] = left[l++];
				continue;
			}

			arr[k++] = right[r++];
		}

		while (l < left.Length)
		{
			arr[k++] = left[l++];
		}

		while (r < right.Length)
		{
			arr[k++] = right[r++];
		}

		return arr;
	}

	static int[] MergeSort(int[] arr, int item)
	{
		var sortedL = arr.Count() > 1 ? MergeSort(arr[..^1], arr[^1]) : arr;

		int i = 0;
		var isInserted = false;
		var sorted = new List<int>();
		while (i < sortedL.Length)
		{
			if (!isInserted && item < sortedL[i])
			{
				sorted.Add(item);
				isInserted = true;
				continue;
			}

			sorted.Add(sortedL[i++]);
		}

		if (!isInserted)
		{
			sorted.Add(item);
		}

		return sorted.ToArray();
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
