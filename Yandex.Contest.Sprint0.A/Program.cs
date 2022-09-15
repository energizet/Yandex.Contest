using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Contest.Sprint0.A
{
	internal class Program
	{

		/// <summary>
		/// Это ваша первая задача. В ней вам придётся прочитать два числа и сложить их. Результат необходимо вывести на стандартный поток вывода или в файл, указанный в условии задачи.
		/// Рекомендуем воспользоваться заготовками кода для данной задачи, лежащими в репозитории курса.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var a = int.Parse(Console.ReadLine());
			var b = int.Parse(Console.ReadLine());
			Console.WriteLine(a + b);
		}
	}
}
