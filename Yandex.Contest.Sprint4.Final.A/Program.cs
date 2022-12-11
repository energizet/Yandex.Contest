using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// https://contest.yandex.ru/contest/24414/run-report/72984989/
/// 
/// -- ПРИНЦИП РАБОТЫ --
/// 
/// Создаётся SearchEngine
///
/// В него добавляются документы, во время добавления сохраняются
/// - слова в документе и количество повторений в этом документе
/// - список словарей в которых находится слово
///
/// Во время поиска
/// - выбираются уникальные поисковые слова
/// - по каждому слову берутся документы в которых есть это слово
/// - по каждому документу берётся релевантность текущего слова и добавляется к релевантности документа
///
/// Сортируем по убыванию релевантности и возрастанию индекса
/// Выбираем первые 5 документов
/// И возвращаем их индексы
/// 
/// -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
///
/// Поскольку на этапе создания запоминается в каких документах находится слово
/// при поиске можно получать релевантность для тех документов в которых есть это слово
/// и не перебирать лишние документы
/// 
/// -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
///
/// Добавление документа проходится за O(n) - где n, количество слов в документе
///
/// Поиск релевантных документов
/// - проход по поисковому запросу в худшем случае O(M), если все слова уникальны - где M количество слов в запросе
/// - проход по документам в худшем случае O(N), если слово есть во всех документах - где N количество документов
///
/// Общая сложность O(M + N), если все слова уникальны и каждое есть во всех документах
/// но из условий задачи M существенно меньше N, поэтому M можно пренебречь
/// Итоговая сложность O(N)
/// 
/// -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
///
/// Дополнительная память
/// - на хранение документов O(N * M) - где N количество документов, а M количество слов в документе
/// - на хранение списков документов где найдены слова O(N * M)
/// в SearchEngine O(N * M + N * M) -> O(2 * N * M) -> O(N * M)
///
/// При поиске
/// - на хранение слов O(K) - где K количество слов в поиске
/// - на хранение результатов O(N) - где N количество документов
/// при поиске O(K + N)
///
/// Всего O(N * M + K + N) -> O(2N * M + K) -> O(n)
/// 
/// </summary>
class SearchEngine
{
	private readonly List<Dictionary<string, int>> _documents = new();
	private readonly Dictionary<string, HashSet<int>> _wordInDocuments = new();

	public SearchEngine Add(IEnumerable<string> document)
	{
		var documentIndexes = new Dictionary<string, int>();

		foreach (var word in document)
		{
			if (!documentIndexes.ContainsKey(word))
			{
				documentIndexes[word] = 0;
			}

			if (!_wordInDocuments.ContainsKey(word))
			{
				_wordInDocuments[word] = new();
			}

			documentIndexes[word]++;
			_wordInDocuments[word].Add(_documents.Count);
		}

		_documents.Add(documentIndexes);
		return this;
	}

	public IEnumerable<int> SearchRelevant(IEnumerable<string> search)
	{
		var searchSet = new HashSet<string>();
		var reses = new Dictionary<int, int>();
		foreach (var searchItem in search)
		{
			if (searchSet.Contains(searchItem))
			{
				continue;
			}

			searchSet.Add(searchItem);

			if (!_wordInDocuments.ContainsKey(searchItem))
			{
				continue;
			}

			var documents = _wordInDocuments[searchItem].Select(index => new
			{
				Index = index,
				Document = _documents[index],
			});

			foreach (var document in documents)
			{
				if (!reses.ContainsKey(document.Index))
				{
					reses[document.Index] = 0;
				}

				reses[document.Index] += document.Document[searchItem];
			}
		}

		return reses.Select(item => (
				Relevant: -item.Value,
				Index: item.Key + 1
			))
			.OrderBy(item => item)
			.Take(5)
			.Select(item => item.Index);
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var searchEngine = new SearchEngine();

		var n = Contest.GetValue(int.Parse);
		for (int i = 0; i < n; i++)
		{
			searchEngine.Add(Contest.GetValue().Split(" "));
		}

		var stringBuilder = new StringBuilder();

		n = Contest.GetValue(int.Parse);
		for (int i = 0; i < n; i++)
		{
			var document = Contest.GetValue().Split(" ");
			var relevantDocuments = searchEngine.SearchRelevant(document);
			stringBuilder.Append(relevantDocuments.Join());
			stringBuilder.Append('\n');
		}

		stringBuilder.ToString().Print();
	}
}

/// <summary>
/// Используется для работы с потоками IO
/// Представляет из себя библеотеку для получения и вывода стандартных структур контеста
/// При необходимости можно легко переделать на файлы и это никак не отразится на работоспособности
/// </summary>
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

	public static List<T> GetList<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.Select(func)
			.ToList();
	}

	public static List<string> GetList(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetValue()
			.Split(new[] {' ', '\t'}, splitOptions)
			.ToList();
	}

	public static T[] GetArray<T>(Func<string, T> func, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(func, splitOptions).ToArray();
	}

	public static string[] GetArray(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
	{
		return GetList(splitOptions).ToArray();
	}

	public static string Join<T>(this IEnumerable<T> arr, string separator = " ")
	{
		return string.Join(separator, arr);
	}

	public static void Print(this string str)
	{
		Console.WriteLine(str);
	}

	public static void Print<T>(this IEnumerable<T> arr, string separator = " ")
	{
		arr.Join().Print();
	}
}