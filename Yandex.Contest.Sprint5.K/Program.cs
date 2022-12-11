// See https://aka.ms/new-console-template for more information

var node1 = new Node(7);
var node2 = new Node(3);
var node3 = new Node(1);
var node4 = new Node(11);
var node5 = new Node(15);
var node6 = new Node(8);
var node7 = new Node(8);

node1.Left = node2;
node1.Right = node4;
node2.Left = node3;
node2.Right = null;
node3.Left = null;
node3.Right = null;
node4.Left = null;
node4.Right = node5;
node5.Left = null;
node5.Right = null;
node6.Left = null;
node6.Right = null;

var left = 6;
var right = 14;

var buffer = new MemoryStream();
var streamWriter = new StreamWriter(buffer);

Solution.PrintRange(node1, left, right, streamWriter);
streamWriter.Flush();

buffer.Position = 0;
var reader = new StreamReader(buffer);
Console.WriteLine(reader.ReadToEnd());