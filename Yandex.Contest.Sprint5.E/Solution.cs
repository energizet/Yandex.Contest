using System;

public class Solution
{
	public static bool Solve(Node root)
	{
		if (root == null)
		{
			return true;
		}

		var leftMax = GetMax(root.Left);
		var rightMax = GetMax(root.Right);

		var left = leftMax < root.Value;
		var right = rightMax < 0 || rightMax > root.Value;

		return left && right && Solve(root.Left) && Solve(root.Right);
	}

	public static int GetMax(Node root)
	{
		if (root == null)
		{
			return -1;
		}

		var leftMax = GetMax(root.Left);
		var rightMax = GetMax(root.Right);
		return Math.Max(Math.Max(leftMax, rightMax), root.Value);
	}
}