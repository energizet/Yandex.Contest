using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution<TValue>
{
	public static void Solve(Node<TValue> head)
	{
		var node = head;
		while (node != null)
		{
			Console.WriteLine(node.Value);
			node = node.Next;
		}
	}
}