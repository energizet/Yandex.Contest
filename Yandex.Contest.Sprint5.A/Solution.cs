using System;

public class Solution
{
	public static int Solve(Node root)
	{
		if (root == null)
		{
			return -1;
		}

		var leftMax = Solve(root.Left);
		var rightMax = Solve(root.Right);
		return Math.Max(Math.Max(leftMax, rightMax), root.Value);
	}
}