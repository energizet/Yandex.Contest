using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Contest.Sprint0.D
{
	internal class Program
	{
		/// <summary>
		/// Рита и Гоша играют в игру. У Риты есть n фишек, на каждой из которых написано количество очков. 
		/// Сначала Гоша называет число k, затем Рита должна выбрать две фишки, сумма очков на которых равна 
		/// заданному числу.
		/// Рите надоело искать фишки самой, и она решила применить свои навыки программирования для решения 
		/// этой задачи.Помогите ей написать программу для поиска нужных фишек.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var n = int.Parse(Console.ReadLine());
			var arr = Console.ReadLine()
				.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();
			var res = int.Parse(Console.ReadLine());

			for (int i = 0; i < n; i++)
			{
				for (int j = i + 1; j < n; j++)
				{
					if (arr[i] + arr[j] == res)
					{
						Console.WriteLine($"{arr[i]} {arr[j]}");
						return;
					}
				}
			}
			Console.WriteLine("None");
		}
	}
}
