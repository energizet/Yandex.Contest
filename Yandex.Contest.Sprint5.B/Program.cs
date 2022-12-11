// See https://aka.ms/new-console-template for more information

{
	var node1 = new Node(1);
	var node2 = new Node(-5);
	var node3 = new Node(3)
	{
		Left = node1,
		Right = node2
	};
	var node4 = new Node(10);
	var node5 = new Node(2)
	{
		Left = node3,
		Right = node4
	};
	Console.WriteLine(Solution.Solve(node5));
	Console.WriteLine(Solution.GetMinDeep(node5, 1));
	Console.WriteLine(Solution.GetMaxDeep(node5, 1));
}

{
	var nodes = new[]
	{
		new Node(0),
		new Node(1),
		new Node(2),
		new Node(3),
		new Node(4),
		new Node(5),
		new Node(6),
		new Node(7),
		new Node(8),
	};

	nodes[0].Left = nodes[1];
	nodes[0].Right = nodes[2];
	nodes[1].Left = nodes[3];
	nodes[1].Right = null;
	nodes[2].Left = null;
	nodes[2].Right = nodes[4];
	nodes[3].Left = nodes[5];
	nodes[3].Right = nodes[6];
	nodes[4].Left = nodes[7];
	nodes[4].Right = nodes[8];
	
	Console.WriteLine(Solution.Solve(nodes[0]));
	Console.WriteLine(Solution.GetMinDeep(nodes[0], 1));
	Console.WriteLine(Solution.GetMaxDeep(nodes[0], 1));
}