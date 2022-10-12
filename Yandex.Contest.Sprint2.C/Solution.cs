using System;

public class Solution<TValue>
{
	public static Node<TValue> Solve(Node<TValue> head, int idx)
	{
		if (idx == 0)
		{
			return head.Next;
		}

		var node = head;
		var i = 1;
		while (node != null)
		{
			if (i == idx)
			{
				node.Next = node.Next.Next;
				break;
			}

			node = node.Next;
			i++;
		}
		return head;
	}
}