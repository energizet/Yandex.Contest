using System;
using System.IO;
using System.Collections.Generic;

public class Solution
{
	public static int SiftDown(List<int> array, int idx)
	{
		var leftIdx = idx * 2;
		var rightIdx = idx * 2 + 1;

		var item = array[idx];

		if (leftIdx >= array.Count)
		{
			return idx;
		}

		var left = array[leftIdx];

		if (rightIdx >= array.Count)
		{
			if (left <= item)
			{
				return idx;
			}

			array[idx] = left;
			array[leftIdx] = item;
			return leftIdx;
		}

		var right = array[rightIdx];

		var maxIdx = idx;
		if (left > array[maxIdx])
		{
			maxIdx = leftIdx;
		}

		if (right > array[maxIdx])
		{
			maxIdx = rightIdx;
		}

		if (maxIdx == idx)
		{
			return idx;
		}

		array[idx] = array[maxIdx];
		array[maxIdx] = item;
		return SiftDown(array, maxIdx);
	}
}