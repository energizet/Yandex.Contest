namespace Yandex.Contest.Sprint3.Final.A;

public class Program
{
	static void Main(string[] args)
	{
		var arr = new List<int> {19, 21, 100, 101, /**/ 1, 4, 5, 7, 12}; //, 19, 21, 100, 101
		Console.WriteLine(Solution.BrokenSearch(arr, 5) == 6);
		arr = new List<int> {5, 1};
		Console.WriteLine(Solution.BrokenSearch(arr, 1) == 1);
		arr = new List<int> {1, 5, 6};
		Console.WriteLine(Solution.BrokenSearch(arr, 1) == 0);
		arr = new List<int> {5, 1};
		Console.WriteLine(Solution.BrokenSearch(arr, 5) == 0);
		arr = new List<int> {1, 5};
		Console.WriteLine(Solution.BrokenSearch(arr, 2) == -1);
	}
}