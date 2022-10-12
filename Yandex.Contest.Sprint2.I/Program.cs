using System;
using System.Collections.Generic;
using System.Linq;

class MyQueueSized
{
	private int[] arr;
	private int size = 0;
	private int pushIndex = 0;
	private int popIndex = 0;

	public MyQueueSized(int maxSize)
	{
		arr = new int[maxSize];
	}

	public void Push(int item)
	{
		if (size >= arr.Length)
		{
			throw new Exception("error");
		}

		arr[pushIndex] = item;
		pushIndex = (pushIndex + 1) % arr.Length;
		size++;
	}

	public int Pop()
	{
		if (size <= 0)
		{
			throw new Exception("None");
		}

		var item = arr[popIndex];
		popIndex = (popIndex + 1) % arr.Length;
		size--;
		return item;
	}

	public int Peek()
	{
		if (size <= 0)
		{
			throw new Exception("None");
		}

		return arr[popIndex];
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
		var maxSize = GetValue(int.Parse);
		var queue = new MyQueueSized(maxSize);

		for (int i = 0; i < count; i++)
		{
			var command = GetList();
			Do(queue, command);
		}
	}

	static void Do(MyQueueSized queue, List<string> command)
	{
		try
		{
			switch (command[0])
			{
				case "push":
					queue.Push(int.Parse(command[1]));
					break;
				case "pop":
					Console.WriteLine(queue.Pop());
					break;
				case "peek":
					Console.WriteLine(queue.Peek());
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
