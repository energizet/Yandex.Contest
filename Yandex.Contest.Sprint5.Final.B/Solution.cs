using System;
using System.IO;

/// <summary>
/// https://contest.yandex.ru/contest/24810/run-report/79245019/
/// 
/// -- ПРИНЦИП РАБОТЫ --
/// 
/// 1) поиск удаяемого элемента
/// 2) поиск у удаляемого элемента
///		самого левого элемента в правой ветке, либо самого правого в левой ветке
/// 3) у найденого элемента переприсваем ребёнка на место этого элемента
/// 4) найденный элемент вставляем на место удаляемого
/// 
/// Пустое дерево:
/// вернёт null
///
/// Лист:
/// Алгоритм рукурсивно спускается до искомого элемента
/// и просто удаляет элемент, так как у него нет потомков
///
/// Элемент с потомками:
/// Аналогочно листу ищет элемент
/// если левая ветка существует - выберается правый элемент в левой ветке
/// если нет - проверяется правая ветка и вернётся левый элемент правой ветки
/// если и правой нет - это считается листом
/// 
/// Корень:
/// Поиск не производится, ведь искомый элемент это корень
/// запускается алгоритм поиска кандидата на замену аналогично элементам с потомками
/// если у корня нет потомков - вернётся null
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// O(h) - на поиск удаляемого элемента
/// O(h) - на поиск кандидата на замену
/// O(1) - на замену удаляемого элемента кандидатом
/// Всего O(h)
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// O(h) - алгоритм потребляет память для указателей на элементы в рекурсии
/// хоть указатели существенно меньше самого элемента,
/// тем не менее зависимость ленейна от высоты дерева
/// 
/// </summary>
public class Solution
{
	public static Node Remove(Node root, int key)
	{
		if (root == null)
		{
			return null;
		}

		if (root.Value == key)
		{
			return GetNodeToReconnect(root);
		}

		if (key < root.Value)
		{
			root.Left = Remove(root.Left, key);
		}

		if (root.Value < key)
		{
			root.Right = Remove(root.Right, key);
		}

		return root;
	}

	private static Node GetNodeToReconnect(Node root)
	{
		//поиск сомого правого элемента в левой ветке
		if (root.Left != null)
		{
			var mostRight = GetMostRightNodeAndReconnect(root.Left);
			if (mostRight != root.Left)
			{
				mostRight.Left = root.Left;
			}

			mostRight.Right = root.Right;
			return mostRight;
		}

		//если левой ветки - нет, ищем самый левый элемент в правом дереве
		if (root.Right != null)
		{
			var mostLeft = GetMostLeftNodeAndReconnect(root.Right);
			mostLeft.Left = root.Left;
			if (mostLeft != root.Right)
			{
				mostLeft.Right = root.Right;
			}

			return mostLeft;
		}

		return null;
	}

	/// <summary>
	/// Возвращает элемент который можно использовать для замены удаляемого
	/// и перекидывает детей этого элемента на его родителя
	/// </summary>
	/// <param name="root"></param>
	/// <returns></returns>
	private static Node GetMostRightNodeAndReconnect(Node root)
	{
		//перемещаем root на правого потомка
		var parent = root;
		root = root.Right;

		//если root пустой, значит изначально parent нет правых потомков
		if (root == null)
		{
			return parent;
		}

		//перемещаем parent и root до тех пор, пока у root не будет правого потомка
		while (root.Right != null)
		{
			parent = root;
			root = root.Right;
		}

		//присваиваем parent левого потомка на место root
		parent.Right = root.Left;
		return root;
	}

	private static Node GetMostLeftNodeAndReconnect(Node root)
	{
		var parent = root;
		root = root.Left;

		if (root == null)
		{
			return parent;
		}

		while (root.Left != null)
		{
			parent = root;
			root = root.Left;
		}

		parent.Left = root.Right;
		return root;
	}
}