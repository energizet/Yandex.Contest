using System;
using System.IO;

public class Solution
{
	public static bool Solve(Node root)
	{
		var minLeftDeep = GetMinDeep(root.Left, 2);
		var maxLeftDeep = GetMaxDeep(root.Left, 2);

		var minRightDeep = GetMinDeep(root.Right, 2);
		var maxRightDeep = GetMaxDeep(root.Right, 2);

		var minDeep = Math.Min(minLeftDeep, minRightDeep);
		var maxDeep = Math.Max(maxLeftDeep, maxRightDeep);

		return maxDeep - minDeep < 2;
	}

	public static int GetMinDeep(Node root, int currentDeep)
	{
		if (root == null)
		{
			return currentDeep - 1;
		}

		var leftMinDeep = GetMinDeep(root.Left, currentDeep + 1);
		var rightMinDeep = GetMinDeep(root.Right, currentDeep + 1);

		if (leftMinDeep < rightMinDeep)
		{
			return leftMinDeep;
		}

		return rightMinDeep;
	}

	public static int GetMaxDeep(Node root, int currentDeep)
	{
		if (root == null)
		{
			return currentDeep - 1;
		}

		var leftMinDeep = GetMaxDeep(root.Left, currentDeep + 1);
		var rightMinDeep = GetMaxDeep(root.Right, currentDeep + 1);

		if (leftMinDeep > rightMinDeep)
		{
			return leftMinDeep;
		}

		return rightMinDeep;
	}
}