using System;

public class Solution
{
	public static int Solve(Node<string> head, string item)
	{
		var node = head;
		var i = 0;
		while (node != null)
		{
			if (item == node.Value)
			{
				return i;
			}

			node = node.Next;
			i++;
		}
		return -1;
	}
}