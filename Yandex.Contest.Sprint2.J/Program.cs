using System;
using System.Collections.Generic;
using System.Linq;

class MyQueueList
{
	private LinkedList<int> queue;
	private int size = 0;

	public MyQueueList()
	{
		queue = new LinkedList<int>();
	}

	public void Put(int item)
	{
		queue.AddLast(item);
		size++;
	}

	public int Get()
	{
		if (size <= 0)
		{
			throw new Exception("error");
		}

		var item = queue.First.Value;
		queue.RemoveFirst();
		size--;
		return item;
	}

	public int Size()
	{
		return size;
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var count = GetValue(int.Parse);
		var queue = new MyQueueList();

		for (int i = 0; i < count; i++)
		{
			var command = GetList();
			Do(queue, command);
		}
	}

	static void Do(MyQueueList queue, List<string> command)
	{
		try
		{
			switch (command[0])
			{
				case "put":
					queue.Put(int.Parse(command[1]));
					break;
				case "get":
					Console.WriteLine(queue.Get());
					break;
				case "size":
					Console.WriteLine(queue.Size());
					break;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
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
