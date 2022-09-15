using System;
using System.Collections.Generic;
using System.Linq;

namespace Yandex.Contest.Sprint1.D
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.ReadLine();
			var arr = GetList(int.Parse);

			var res = GetResult(arr);
			Console.WriteLine(res);
		}

		static int GetResult(List<int> arr)
		{
			if (arr.Count <= 0)
			{
				return 0;
			}

			var haos = 0;

			for (int i = 0; i < arr.Count; i++)
			{
				var leftToUp = i == 0 ? true : arr[i] > arr[i - 1];
				var rightToDown = i == arr.Count - 1 ? true : arr[i] > arr[i + 1];

				if (leftToUp && rightToDown)
				{
					haos++;
				}
			}

			return haos;
		}

		static T GetValue<T>(Func<string, T> func)
		{
			return func(Console.ReadLine());
		}

		static List<T> GetList<T>(Func<string, T> func)
		{
			return Console.ReadLine()
					.Split(new[] { ' ', '\t' })
					.Select(func)
					.ToList();
		}
	}
}
