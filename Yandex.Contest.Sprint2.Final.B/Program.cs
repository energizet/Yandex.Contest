using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://contest.yandex.ru/contest/22781/run-report/71119575/
/// 
/// не совсем понимаю, что здесь нужно описывать
/// сделал так как и описанно в задаче
/// 
/// если число - вставляем в стек
/// если знак - вытаскиваем сначала правый операнд, а за ним левый
/// со сложением и умножением разницы - нет
/// а с отниманием нужно поменять местами получение
/// с делением воспользоваться Math.Floor
/// 
/// скорость - O(n)
/// память - O(n)
/// </summary>
internal class Program
{
	static void Main(string[] args)
	{
		var arr = GetList();
		var res = Calculate(arr);

		Console.WriteLine(res);
	}

	static int Calculate(List<string> arr)
	{
		var stack = new Stack<int>(new[] { 0 });

		foreach (var item in arr)
		{
			var res = item switch
			{
				"+" => stack.Pop() + stack.Pop(),
				"*" => stack.Pop() * stack.Pop(),
				"-" => -stack.Pop() + stack.Pop(),
				"/" => ((Func<int>)(() =>
				{
					var right = stack.Pop();
					var left = stack.Pop();
					var res = 1.0 * left / right;
					return (int)Math.Floor(res);
				}))(),
				_ => int.Parse(item),
			};

			stack.Push(res);
		}

		return stack.Pop();
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

	static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return Console.ReadLine()
				.Split(new[] { ' ', '\t' }, splitOptions)
				.ToList();
	}
}
