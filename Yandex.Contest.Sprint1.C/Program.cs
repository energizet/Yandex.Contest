using System;
using System.Collections.Generic;
using System.Linq;

namespace Yandex.Contest.Sprint1.C
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var (rows, cells) = (int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));

			var matrix = new List<List<int>>();

			for (int i = 0; i < rows; i++)
			{
				var arr = Console.ReadLine()
					.Split(new[] { ' ', '\t' })
					.Select(int.Parse)
					.ToList();

				matrix.Add(arr);
			}

			var (y, x) = (int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));

			var res = GetResult(matrix, rows, cells, x, y);
			res.Sort();

			Console.WriteLine(string.Join(" ", res));
		}

		private static List<int> GetResult(List<List<int>> matrix, int rows, int cells, int x, int y)
		{
			var arr = new List<int>();

			if (rows <= 0 || cells <= 0)
			{
				return arr;
			}

			if (x > 0)
			{
				arr.Add(matrix[y][x - 1]);
			}

			if (y > 0)
			{
				arr.Add(matrix[y - 1][x]);
			}

			if (x < cells - 1)
			{
				arr.Add(matrix[y][x + 1]);
			}

			if (y < rows - 1)
			{
				arr.Add(matrix[y + 1][x]);
			}

			return arr;
		}
	}
}
