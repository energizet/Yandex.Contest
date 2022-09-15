using System;
using System.Linq;

namespace Yandex.Contest.Sprint1.B
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var arr = Console.ReadLine()
				.Split(new[] { ' ', '\t' })
				.Select(int.Parse)
				.ToList();

			var res = GetResult(arr[0], arr[1], arr[2]);
			Console.WriteLine(res ? "WIN" : "FAIL");
		}

		static bool GetResult(int a, int b, int c)
		{
			a = Math.Abs(a);
			b = Math.Abs(b);
			c = Math.Abs(c);
			return a % 2 == b % 2 && b % 2 == c % 2;
		}
	}
}
