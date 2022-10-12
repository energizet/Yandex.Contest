using System;

namespace Yandex.Contest.Sprint2.E
{
	internal class Program
	{
		static void Main(string[] args)
        {
            var node3 = new Node<string>("node3", null, null);
            var node2 = new Node<string>("node2", node3, null);
            var node1 = new Node<string>("node1", node2, null);
            var node0 = new Node<string>("node0", node1, null);
            var newNode = Solution.Solve(node0);
            /*
            result is : newNode == node3
            node3.next == node2
            node2.next == node1
            node2.prev == node3
            node1.next == node0
            node1.prev == node2
            node0.prev == node1
            */
            for (var node = newNode; node != null; node = node.Next)
            {
                Console.WriteLine(node.Value);
            }
        }
    }
}
