// See https://aka.ms/new-console-template for more information

var sample = new List<int> {-1, 12, 6, 8, 3, 15, 7};
Console.WriteLine(string.Join(", ", sample));
Console.WriteLine(Solution.SiftUp(sample, 5) == 1);
Console.WriteLine(string.Join(", ", sample));