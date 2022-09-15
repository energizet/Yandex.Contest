using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Contest.Sprint0.B
{
	internal class Program
	{
		/// <summary>
		/// Даны два массива чисел длины n. Составьте из них один массив длины 2n, в котором числа из входных массивов чередуются (первый — второй — первый — второй — ...).
		/// При этом относительный порядок следования чисел из одного массива должен быть сохранён.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var n = int.Parse(Console.ReadLine());
			var arr1 = Console.ReadLine()
				.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();
			var arr2 = Console.ReadLine()
				.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			var outArr = new List<int>();
			for (int i = 0; i < n; i++)
			{
				outArr.Add(arr1[i]);
				outArr.Add(arr2[i]);
			}

			Console.WriteLine(string.Join(" ", outArr));
		}
	}
}
