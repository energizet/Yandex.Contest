using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://contest.yandex.ru/contest/23815/run-report/72529634/
/// Сначала бинарным поиском находим сдвиг реального начала массива
/// После ищем элемент учитывая сдвиг
///
/// CPU - O(log n)
/// Память - O(1)
/// </summary>
public class Solution
{
	public static int BrokenSearch(List<int> array, int el)
	{
		var start = FindStart(array, 0, array.Count);
		return FindIndex(array, el, 0, array.Count, start);
	}

	static int FindStart(List<int> array, int left, int right)
	{
		if (right - left <= 1)
		{
			return right;
		}

		var mid = (left + right) / 2;

		if (array[left] > array[mid])
		{
			return FindStart(array, left, mid);
		}

		return FindStart(array, mid, right);
	}

	static int FindIndex(List<int> array, int item, int left, int right, int shift)
	{
		if (right - left == 0)
		{
			return -1;
		}

		if (right - left == 1)
		{
			left += shift;
			return item == array[left % array.Count] ? left % array.Count : -1;
		}

		left += shift;
		right += shift;
		var mid = (left + right) / 2;

		if (item < array[mid % array.Count])
		{
			return FindIndex(array, item, left - shift, mid - shift, shift);
		}

		if (item > array[mid % array.Count])
		{
			return FindIndex(array, item, mid - shift, right - shift, shift);
		}

		return mid % array.Count;
	}
}