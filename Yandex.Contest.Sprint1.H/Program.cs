using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
	static void Main(string[] args)
	{
		var num1 = Console.ReadLine();
		var num2 = Console.ReadLine();

		var res = GetResult(num1, num2);
		Console.WriteLine(res);
	}

	static string GetResult(string str1, string str2)
	{
		var length = Math.Max(str1.Length, str2.Length);
		str1 = $"{new string('0', length - str1.Length)}{str1}";
		str2 = $"{new string('0', length - str2.Length)}{str2}";

		var nums1 = str1.Select(item => item - 48).ToList();
		var nums2 = str2.Select(item => item - 48).ToList();

		var sumNums = new List<int>();

		var plus = 0;
		for (int i = 0; i < length; i++)
		{
			var num1 = nums1[length - 1 - i];
			var num2 = nums2[length - 1 - i];

			var sum = num1 + num2 + plus;
			if (sum >= 2)
			{
				plus = 1;
				sumNums.Add(sum - 2);
				continue;
			}

			plus = 0;
			sumNums.Add(sum);
		}

		if (plus > 0)
		{
			sumNums.Add(plus);
		}
		sumNums.Reverse();

		return string.Join("", sumNums);
	}

	static T GetValue<T>(Func<string, T> func)
	{
		return func(Console.ReadLine());
	}

	static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.Select(func)
				.ToList();
	}

	static List<string> GetList<T>(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.ToList();
	}
}
