// See https://aka.ms/new-console-template for more information

var sample = new List<int> {-1, 12, 1, 8, 3, 4, 7};
Console.WriteLine(string.Join(", ", sample));
Console.WriteLine(Solution.SiftDown(sample, 2) == 5);
Console.WriteLine(string.Join(", ", sample));