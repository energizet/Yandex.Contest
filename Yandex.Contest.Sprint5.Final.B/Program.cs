// See https://aka.ms/new-console-template for more information

var node7 = new Node(5)
{
	Left = new Node(1)
	{
		Right = new Node(3)
		{
			Left = new Node(2)
		}
	},
	Right = new Node(10)
	{
		Left = new Node(8)
		{
			Left = new Node(6)
		}
	}
};

var node5 = node7.Right.Left;
var newHead = Solution.Remove(node7, 10);

Console.WriteLine(newHead.Value == 5);
Console.WriteLine(newHead.Right == node5);
Console.WriteLine(newHead.Right.Value == 8);