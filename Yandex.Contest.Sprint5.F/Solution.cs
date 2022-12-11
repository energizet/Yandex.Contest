using System;
using System.IO;

public class Solution
{
	public static int Solve(Node root, int depth = 1)
	{
		if (root == null)
		{
			return depth - 1;
		}

		var leftMinDeep = Solve(root.Left, depth + 1);
		var rightMinDeep = Solve(root.Right, depth + 1);

		if (leftMinDeep > rightMinDeep)
		{
			return leftMinDeep;
		}

		return rightMinDeep;
	}
}