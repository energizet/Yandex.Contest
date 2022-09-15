using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
	static void Main(string[] args)
	{
		Console.ReadLine();
		var homes = GetList(int.Parse);

		var res = GetResult(homes);
		Console.WriteLine(string.Join(" ", res));
	}

	static List<int> GetResult(List<int> homes)
	{
		var res = Enumerable.Repeat(-1, homes.Count).ToList();

		var lZero = -1;
		var rZero = -1;
		for (int li = 0, ri = homes.Count - 1; li < homes.Count; li++, ri--)
		{
			var l = homes[li];
			var r = homes[ri];
			var lRes = res[li];
			var rRes = res[ri];

			if (l == 0)
			{
				lZero = li;
			}

			if (r == 0)
			{
				rZero = ri;
			}

			var lAbs = li - lZero;
			if (lZero >= 0 && (lRes < 0 || lAbs < lRes))
			{
				res[li] = lAbs;
			}

			var rAbs = rZero - ri;
			if (rZero >= 0 && (rRes < 0 || rAbs < rRes))
			{
				res[ri] = rAbs;
			}

			if (li == ri && lAbs >= 0 && rAbs >= 0)
			{
				res[li] = Math.Min(lAbs, rAbs);
			}
		}

		return res;
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
