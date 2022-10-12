using System;

public class Solution
{
	public static Node<string> Solve(Node<string> head)
	{
		var prev = (Node<string>)null;
		var node = head;
		while (node != null)
		{
			node.Prev = prev;

			var tmp = node.Prev;
			node.Prev = node.Next;
			node.Next = tmp;

			prev = node;
			node = node.Prev;
		}
		return prev;
	}
}