using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://contest.yandex.ru/contest/22781/run-report/71115097/
/// 
/// Я храню индекс начала и индекс окончания деки
/// Начало и конец указывают на настоящие индексы
/// 
/// При добавлении в начало нужно вычислить предыдущий индекс, а в конец - следующий, затем вставить значени
/// 
/// При извлечении из начала нужно сначала извлечь элемент, а затем вычислить следующий индекс, а из конца - предыдущий
/// 
/// Скорость всех операций - O(1)
/// Занимаемая память - O(n)
/// </summary>
class MyDecSized
{
	private int[] arr;
	private int size = 0;
	private int startIndex = 0;
	private int endIndex;

	public MyDecSized(int maxSize)
	{
		arr = new int[maxSize];
		endIndex = maxSize - 1;
	}

	public bool TryPushBask(int item)
	{
		if (IsFull())
		{
			return false;
		}

		endIndex = (endIndex + 1) % arr.Length;
		arr[endIndex] = item;
		size++;

		return true;
	}

	public bool TryPushFront(int item)
	{
		if (IsFull())
		{
			return false;
		}

		startIndex = startIndex - 1 < 0 ? arr.Length - 1 : startIndex - 1;
		arr[startIndex] = item;
		size++;

		return true;
	}

	public bool TryPopFront(out int result)
	{
		if (IsEmpty())
		{
			result = 0;
			return false;
		}

		result = arr[startIndex];
		startIndex = (startIndex + 1) % arr.Length;
		size--;

		return true;
	}

	public bool TryPopBack(out int result)
	{
		if (IsEmpty())
		{
			result = 0;
			return false;
		}

		result = arr[endIndex];
		endIndex = endIndex - 1 < 0 ? arr.Length - 1 : endIndex - 1;
		size--;

		return true;
	}

	public void PushBack(int item)
	{
		if (!TryPushBask(item))
		{
			throw new InvalidOperationException("error");
		}
	}

	public void PushFront(int item)
	{
		if (!TryPushFront(item))
		{
			throw new InvalidOperationException("error");
		}
	}

	public int PopFront()
	{
		if (!TryPopFront(out var result))
		{
			throw new InvalidOperationException("error");
		}

		return result;
	}

	public int PopBack()
	{
		if (!TryPopBack(out var result))
		{
			throw new InvalidOperationException("error");
		}

		return result;
	}

	public bool IsEmpty()
	{
		return size <= 0;
	}

	public bool IsFull()
	{
		return size >= arr.Length;
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var count = GetValue(int.Parse);
		var maxSize = GetValue(int.Parse);
		var queue = new MyDecSized(maxSize);

		for (int i = 0; i < count; i++)
		{
			var command = GetList();
			ExecuteCommand(queue, command);
		}
	}

	static void ExecuteCommand(MyDecSized queue, List<string> command)
	{
		try
		{
			switch (command[0])
			{
				case "push_back":
					queue.PushBack(int.Parse(command[1]));
					break;
				case "push_front":
					queue.PushFront(int.Parse(command[1]));
					break;
				case "pop_front":
					Console.WriteLine(queue.PopFront());
					break;
				case "pop_back":
					Console.WriteLine(queue.PopBack());
					break;
			}
		}
		catch (InvalidOperationException ex)
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
