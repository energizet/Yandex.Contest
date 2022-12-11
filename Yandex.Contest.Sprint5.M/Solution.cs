using System;
using System.IO;
using System.Collections.Generic;

public class Solution
{
	public static int SiftUp(List<int> array, int idx)
	{
		if (idx == 1)
		{
			return 1;
		}

		var parentIdx = idx / 2;
		var item = array[idx];
		var parent = array[parentIdx];

		if (item <= parent)
		{
			return idx;
		}

		array[parentIdx] = item;
		array[idx] = parent;
		return SiftUp(array, parentIdx);
	}
}