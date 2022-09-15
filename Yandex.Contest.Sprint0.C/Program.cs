using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Contest.Sprint0.C
{
	internal class Program
	{
		/// <summary>
		/// Вам дана статистика по числу запросов в секунду к вашему любимому рекомендательному сервису.
		/// Измерения велись n секунд.
		/// В секунду i поступает qi запросов.
		/// Примените метод скользящего среднего с длиной окна k к этим данным и выведите результат.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var n = int.Parse(Console.ReadLine());
			var arr = Console.ReadLine()
				.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();
			var k = int.Parse(Console.ReadLine());

			var outArr = new List<double>();
			var sum = 1.0 * arr.Take(k).Sum();
			outArr.Add(sum / k);

			for (int i = 0; i < n - k; i++)
			{
				sum -= arr[i];
				sum += arr[i + k];
				outArr.Add(sum / k);
			}

			Console.WriteLine(string.Join(" ", outArr));
		}
	}
}
