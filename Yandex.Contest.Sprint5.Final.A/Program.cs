using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://contest.yandex.ru/contest/24810/run-report/79229712/
///
/// -- ПРИНЦИП РАБОТЫ --
/// 
/// Создаётся Heap
///
/// Добявляются пользователи, и одновременно просеиваются вверх
///
/// После, возвращаются все элементы с самого приоритетного, из-за того,
/// что при извлечении производится просеивание вниз - при следующем извлечении
/// на первом месте будет самый приоритетный пользователь
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(n * log n) - на добавления всех пользователей
/// O(n * log n) - на извлечение всех пользователей
/// Всего O(n * log n)
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// Для хранения кучи тратится O(n) дополнительной памяти
/// 
/// </summary>
class User : IComparable<User>
{
	public readonly string Name;
	public readonly int Score;
	public readonly int Fine;

	public User(string name, int score, int fine)
	{
		Name = name;
		Score = score;
		Fine = fine;
	}

	public int CompareTo(User? right)
	{
		if (right == null)
		{
			throw new ArgumentNullException($"Compare object cannot be null");
		}

		if (Score != right.Score)
		{
			return -Score.CompareTo(right.Score);
		}

		if (Fine != right.Fine)
		{
			return Fine.CompareTo(right.Fine);
		}

		if (Name != right.Name)
		{
			return string.Compare(Name, right.Name, StringComparison.Ordinal);
		}

		return 0;
	}

	public bool Equals(User? user)
	{
		if (user is null)
		{
			return false;
		}

		return CompareTo(user) == 0;
	}

	public override bool Equals(object? obj)
	{
		return obj is User user && Equals(user);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name, Score, Fine);
	}

	public override string ToString()
	{
		return $"{Name} {Score} {Fine}";
	}

	/// <summary>
	/// Если один из объектов null - вернёт false,
	/// иначе проверка через CompareTo
	/// </summary>
	public static bool operator <(User? left, User? right)
	{
		if (left is null || right is null)
		{
			return false;
		}

		return left.CompareTo(right) > 0;
	}

	/// <summary>
	/// Если один из объектов null - вернёт false,
	/// иначе проверка через CompareTo
	/// </summary>
	public static bool operator >(User? left, User? right)
	{
		if (left is null || right is null)
		{
			return false;
		}

		return left.CompareTo(right) < 0;
	}

	/// <summary>
	/// Если оба объекта null - вернёт true,
	/// иначе инвертированый оператор >
	/// </summary>
	public static bool operator <=(User? left, User? right)
	{
		if (left is null && right is null)
		{
			return true;
		}

		//преведёт к рекурсии и переполнению стека
		//return left <= right;
		return !(left > right);
	}

	/// <summary>
	/// Если оба объекта null - вернёт true,
	/// иначе инвертированый оператор <
	/// </summary>
	public static bool operator >=(User? left, User? right)
	{
		if (left is null && right is null)
		{
			return true;
		}

		return !(left < right);
	}

	/// <summary>
	/// Если оба объекта null - вернёт true,
	/// иначе проверка через Equals
	/// </summary>
	public static bool operator ==(User? left, User? right)
	{
		if (left is null && right is null)
		{
			return true;
		}

		return left is not null && left.Equals(right);
	}

	/// <summary>
	/// Инвертированый оператор ==
	/// </summary>
	public static bool operator !=(User? left, User? right)
	{
		return !(left == right);
	}
}

class Heap
{
	private readonly List<User?> _users = new() {null};

	/// <summary>
	/// Добавляет элемент в конец массива
	/// и начинает просеивание вверх с него
	/// </summary>
	/// <param name="user"></param>
	public void Push(User user)
	{
		_users.Add(user);
		SiftUp(_users.Count - 1);
	}

	/// <summary>
	/// Возвращает верхний элемент
	/// ставит на его место последний
	/// и запускает просеивание вниз
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	public bool TryPop(out User? user)
	{
		if (_users.Count < 2)
		{
			user = null;
			return false;
		}

		user = _users[1];
		_users[1] = _users[^1];
		_users.RemoveAt(_users.Count - 1);

		SiftDown(1);
		return true;
	}

	/// <summary>
	/// Возвращает все элементы в порядке преоритета
	/// </summary>
	/// <returns></returns>
	public IEnumerable<User> PopAll()
	{
		while (TryPop(out var user))
		{
			yield return user!;
		}
	}

	/// <summary>
	/// Поднимает элемент к корню, если он приоритетнее родителя 
	/// </summary>
	/// <param name="idx"></param>
	/// <returns></returns>
	private int SiftUp(int idx)
	{
		if (idx == 1)
		{
			return 1;
		}

		var parentIdx = idx / 2;
		var item = _users[idx];
		var parent = _users[parentIdx];

		if (item <= parent)
		{
			return idx;
		}

		_users[parentIdx] = item;
		_users[idx] = parent;
		return SiftUp(parentIdx);
	}

	/// <summary>
	/// Опескает элемент на место потомноков, если потомок приоритетней элемента
	/// </summary>
	/// <param name="idx"></param>
	/// <returns></returns>
	public int SiftDown(int idx)
	{
		if (idx >= _users.Count)
		{
			return 0;
		}

		var leftIdx = idx * 2;
		var rightIdx = idx * 2 + 1;

		var item = _users[idx];

		if (leftIdx >= _users.Count)
		{
			return idx;
		}

		var left = _users[leftIdx];

		if (rightIdx >= _users.Count)
		{
			if (left <= item)
			{
				return idx;
			}

			_users[idx] = left;
			_users[leftIdx] = item;
			return leftIdx;
		}

		var right = _users[rightIdx];

		var maxIdx = idx;
		if (left > _users[maxIdx])
		{
			maxIdx = leftIdx;
		}

		if (right > _users[maxIdx])
		{
			maxIdx = rightIdx;
		}

		if (maxIdx == idx)
		{
			return idx;
		}

		_users[idx] = _users[maxIdx];
		_users[maxIdx] = item;
		return SiftDown(maxIdx);
	}
}

internal static class Program
{
	static void Main(string[] args)
	{
		var n = Contest.GetValue(int.Parse);
		var heap = new Heap();
		for (int i = 0; i < n; i++)
		{
			var arr = Contest.GetValue().Split(" ");
			var name = arr[0];
			var score = int.Parse(arr[1]);
			var fine = int.Parse(arr[2]);
			heap.Push(new User(name, score, fine));
		}

		heap.PopAll().Select(item => item.Name).Print("\n");
	}
}

static class Contest
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