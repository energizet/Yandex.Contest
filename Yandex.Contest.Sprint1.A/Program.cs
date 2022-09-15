using System;
using System.Linq;

namespace Yandex.Contest.Sprint1.A
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var arr = Console.ReadLine()
				.Split(new[] { ' ', '\t' })
				.Select(int.Parse)
				.ToList();

			var res = GetResult(arr[0], arr[2], arr[3], arr[1]);
			Console.WriteLine(res);
		}

		static int GetResult(int a, int b, int c, int x)
		{
			return a * x * x + b * x + c;
		}
	}
}
