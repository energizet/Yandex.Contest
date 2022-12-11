using System;
using System.IO;

public class Solution
{
	public static void PrintRange(Node root, int left, int right, StreamWriter writer)
	{
		if (root == null)
		{
			return;
		}

		if (left <= root.Value)
		{
			PrintRange(root.Left, left, right, writer);
		}

		if (left <= root.Value && root.Value <= right)
		{
			writer.WriteLine(root.Value);
		}

		if (root.Value <= right)
		{
			PrintRange(root.Right, left, right, writer);
		}
	}
}