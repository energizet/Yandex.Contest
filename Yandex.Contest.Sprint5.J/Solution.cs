using System;
using System.IO;

public class Solution
{
	public static Node Insert(Node root, int key)
	{
		if (root == null)
		{
			return new Node(key);
		}

		if (key < root.Value)
		{
			root.Left = Insert(root.Left, key);
			return root;
		}

		root.Right = Insert(root.Right, key);
		return root;
	}
}