// See https://aka.ms/new-console-template for more information

{
	var node1 = new Node(7);
	var node2 = new Node(8)
	{
		Left = node1,
	};

	var node3 = new Node(7)
	{
		Right = node2,
	};
	var newHead = Solution.Insert(node3, 6);
	Console.WriteLine(newHead == node3);
	Console.WriteLine(newHead.Left.Value == 6);
}
{
	var node1 = new Node(7);
	var node2 = new Node(8)
	{
		Left = node1,
	};

	var node3 = new Node(7)
	{
		Right = node2,
	};
	var newHead = Solution.Insert(node3, 7);
	Console.WriteLine(newHead == node3);
	Console.WriteLine(newHead.Right.Left.Right.Value == 7);
}