﻿// See https://aka.ms/new-console-template for more information

var node1 = new Node(1);
var node2 = new Node(4);
var node3 = new Node(3)
{
	Left = node1,
	Right = node2
};
var node4 = new Node(8);
var node5 = new Node(5)
{
	Left = node3,
	Right = node4
};
Console.WriteLine(Solution.Solve(node5));
node2.Value = 5;
Console.WriteLine(Solution.Solve(node5));