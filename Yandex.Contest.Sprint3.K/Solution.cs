using System.Collections.Generic;
using System.Linq;
using System;

public class Solution
{
	public static void MergeSort(List<int> array, int left, int right)
	{
		if (right - left <= 1)
		{
			return;
		}

		var mid = (left + right) / 2;

		MergeSort(array, left, mid);
		MergeSort(array, mid, right);

		var merged = Merge(array, left, mid, right);

		for (int i = left; i < right; i++)
		{
			array[i] = merged[i - left];
		}
	}

	public static List<int> Merge(List<int> array, int left, int mid, int right)
	{
		var l = left;
		var r = mid;
		var merged = new List<int>();
		while (l < mid && r < right)
		{
			if (array[l] <= array[r])
			{
				merged.Add(array[l++]);
				continue;
			}

			merged.Add(array[r++]);
		}

		while (l < mid)
		{
			merged.Add(array[l++]);
		}

		while (r < right)
		{
			merged.Add(array[r++]);
		}

		return merged;
	}

	public static void Main(string[] args)
	{
		var algo = Console.ReadLine();
		var num = int.Parse(Console.ReadLine());
		var arr = Console.ReadLine()
			.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse)
			.ToList();

		if (algo == "sort")
		{
			MergeSort(arr, 0, arr.Count);
			Console.WriteLine(string.Join(" ", arr));
		}
		else if (algo == "merge")
		{
			Console.WriteLine($"{arr.Count} {num}");
			//var merged = Merge(arr, 0, arr.Count / 2, arr.Count);
			//Console.WriteLine(string.Join(" ", merged));
		}
	}
}