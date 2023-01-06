using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// https://contest.yandex.ru/contest/25597/run-report/80176607/
/// 
/// -- ПРИНЦИП РАБОТЫ --
///
/// Отбрасываем не чётную сумму элементов
/// Наполняем кучу элементами
/// Берём два бОльших элементов и условно помещаем их в две стопки
/// а разницу между элементами кладём обратно в кучу
/// Когда в куче останется один элемент - это будет разница между двумя стопками
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(n * log(n))
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(n) - элементы в куче
/// 
/// </summary>
/// <summary>
/// Куча
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class Heap<T> where T : IComparable<T>
{
	/// <summary>
	/// Дерево кучи
	/// </summary>
	private readonly List<T> _items = new() {default};

	private readonly Func<T, T, bool> _priority;

	public Heap(Func<T, T, bool> priority)
	{
		_priority = priority;
	}

	/// <summary>
	/// Размер очереди
	/// </summary>
	public int Size => _items.Count - 1;

	/// <summary>
	/// Вставка элемента
	/// </summary>
	/// <param name="item"></param>
	public void Push(T item)
	{
		_items.Add(item);
		SiftUp(_items.Count - 1);
	}

	/// <summary>
	/// Множественная вставка
	/// </summary>
	/// <param name="items"></param>
	public void Push(IEnumerable<T> items)
	{
		foreach (var item in items)
		{
			Push(item);
		}
	}

	/// <summary>
	/// Получить элемент с вершины кучи
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool TryPop(out T item)
	{
		if (_items.Count < 2)
		{
			item = default;
			return false;
		}

		item = _items[1];
		_items[1] = _items[^1];
		_items.RemoveAt(_items.Count - 1);

		SiftDown(1);
		return true;
	}

	private int SiftUp(int idx)
	{
		if (idx == 1)
		{
			return 1;
		}

		var parentIdx = idx / 2;
		var item = _items[idx];
		var parent = _items[parentIdx];

		if (_priority(item, parent) == false)
		{
			return idx;
		}

		_items[parentIdx] = item;
		_items[idx] = parent;
		return SiftUp(parentIdx);
	}

	private int SiftDown(int idx)
	{
		if (idx >= _items.Count)
		{
			return 0;
		}

		var leftIdx = idx * 2;
		var rightIdx = idx * 2 + 1;

		var item = _items[idx];

		if (leftIdx >= _items.Count)
		{
			return idx;
		}

		var left = _items[leftIdx];

		if (rightIdx >= _items.Count)
		{
			if (_priority(left, item) == false)
			{
				return idx;
			}

			_items[idx] = left;
			_items[leftIdx] = item;
			return leftIdx;
		}

		var right = _items[rightIdx];

		var maxIdx = idx;
		if (_priority(left, _items[maxIdx]))
		{
			maxIdx = leftIdx;
		}

		if (_priority(right, _items[maxIdx]))
		{
			maxIdx = rightIdx;
		}

		if (maxIdx == idx)
		{
			return idx;
		}

		_items[idx] = _items[maxIdx];
		_items[maxIdx] = item;
		return SiftDown(maxIdx);
	}

	public static bool MaxPriority(T l, T r) => l.CompareTo(r) > 0;
	public static bool MinPriority(T l, T r) => l.CompareTo(r) < 0;
}

internal static class Program
{
	static void Main(string[] args)
	{
		Contest.GetValue();
		var arr = Contest.GetArray(int.Parse);
		IsHalfSubMultiply(arr).Print();
	}

	static bool IsHalfSubMultiply(int[] arr)
	{
		var sum = arr.Sum();
		if (sum % 2 == 1)
		{
			return false;
		}

		var heap = new Heap<int>(Heap<int>.MaxPriority);
		heap.Push(arr);

		while (heap.Size > 1)
		{
			heap.TryPop(out var item1);
			heap.TryPop(out var item2);
			heap.Push(item1 - item2);
		}

		heap.TryPop(out var res);
		return res == 0;
	}
}

/// <summary>
/// I/O методы
/// </summary>
internal static class Contest
{
	public static string GetValue()
	{
		return Console.ReadLine()!;
	}

	public static T GetValue<T>(Func<string, T> func)
	{
		return func(GetValue());
	}

	public static IEnumerable<T> GetIEnumerable<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func);
	}

	public static List<T> GetList<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToList();
	}

	public static List<string> GetList(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(str => str, splitOptions);
	}

	public static T[] GetArray<T>(Func<string, T> func,
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetIEnumerable(func, splitOptions).ToArray();
	}

	public static string[] GetArray(
		StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetArray(str => str, splitOptions);
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join(separator).Print();
	}

	public static void Print(this ValueType value)
	{
		Console.WriteLine(value);
	}

	public static void Print(this object obj)
	{
		Console.WriteLine(obj);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}
}