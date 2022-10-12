using System;

internal class Program
{
	static void Main(string[] args)
	{
		var node3 = new Node<string>("node3", null);
		var node2 = new Node<string>("node2", node3);
		var node1 = new Node<string>("node1", node2);
		var node0 = new Node<string>("node0", node1);
		Solution<string>.Solve(node0);
	}
}
