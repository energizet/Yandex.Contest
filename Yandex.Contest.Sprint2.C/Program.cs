using System;

internal class Program
{
	static void Main(string[] args)
	{
		var node3 = new Node<string>("node3", null);
		var node2 = new Node<string>("node2", node3);
		var node1 = new Node<string>("node1", node2);
		var node0 = new Node<string>("node0", node1);
		var newHead = Solution<string>.Solve(node0, 1);
		// result is : node0 -> node2 -> node3



		for (var node = newHead; node != null; node = node.Next)
		{
			Console.WriteLine(node.Value);
		}
	}
}
