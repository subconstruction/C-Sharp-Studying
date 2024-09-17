using System;
using System.Diagnostics;

namespace Practice_2
{
    public class SearchResult
    {
        public bool IsFine { get; set; }
        public int TargetItem { get; set; }
        public long Runtime { get; set; }

        public void OutputResult()
        {
            if (IsFine)
                Console.WriteLine($"Элемент найден на индексе: {TargetItem}. Runtime: {Runtime} t.");
            else
                Console.WriteLine($"Элемент не найден. Runtime: {Runtime} t.");
        }
    }

    public interface ISearcher
    {
        SearchResult Search(int[] targetArray, int target);
        string Name { get; }
    }

    public class LinearSearcher : ISearcher
    {
        public string Name => "Линейный поиск";

        public SearchResult Search(int[] targetArray, int target)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < targetArray.Length; i++)
            {
                if (targetArray[i] == target)
                {
                    stopwatch.Stop();
                    return new SearchResult { IsFine = true, TargetItem = i, Runtime = stopwatch.ElapsedTicks };
                }
            }

            stopwatch.Stop();
            return new SearchResult { IsFine = false, TargetItem = -1, Runtime = stopwatch.ElapsedTicks };
        }
    }

    public class BinarySearcher : ISearcher
    {
        public string Name => "Бинарный поиск";
        
        public SearchResult Search(int[] targetArray, int target)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int left = 0;
            int right = targetArray.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (targetArray[mid] == target)
                {
                    stopwatch.Stop();
                    return new SearchResult { IsFine = true, TargetItem = mid, Runtime = stopwatch.ElapsedTicks };
                }
                else if (targetArray[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            stopwatch.Stop();
            return new SearchResult { IsFine = false, TargetItem = -1, Runtime = stopwatch.ElapsedTicks };
        }
    }

    public class DataManager
    {
        private Random _random = new Random();

        public int[] GenerateRandomArray(int size, int minValue, int maxValue)
        {
            int[] targetArray = new int[size];
            for (int i = 0; i < size; i++)
            {
                targetArray[i] = _random.Next(minValue, maxValue + 1);
            }
            return targetArray;
        }

        public int[] GenerateuserChoiceArray(int size, int minValue, int maxValue)
        {
            int[] targetArray = GenerateRandomArray(size, minValue, maxValue);
            Array.Sort(targetArray);
            return targetArray;
        }
    }

    public class SearchService
    {
        private ISearcher _Searcher;
        private DataManager _DataManager = new DataManager();

        public void Set(ISearcher Searcher)
        {
            _Searcher = Searcher;
        }

        public void Do(int[] targetArray, int target)
        {
            Console.WriteLine($"Используем алгоритм: {_Searcher.Name}");
            SearchResult result = _Searcher.Search(targetArray, target);
            result.OutputResult();
        }

        public int[] Generate(int size, bool userChoice)
        {
            if (userChoice)
                return _DataManager.GenerateuserChoiceArray(size, -999, 999);
            else
                return _DataManager.GenerateRandomArray(size, -999, 999);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SearchService SearchService = new SearchService();

            Console.WriteLine("Выберите размер массива:");
            Console.WriteLine("1. 100 элементов");
            Console.WriteLine("2. 1000 элементов");
            Console.WriteLine("3. 10000 элементов");

            int arrayChoice = int.Parse(Console.ReadLine());
            int size = arrayChoice switch
            {
                1 => 100,
                2 => 1000,
                3 => 10000,
                _ => 100
            };

            Console.WriteLine("Вы хотите отсортированный массив для бинарного поиска? (y/n)");
            bool userChoice = Console.ReadLine().ToLower() == "y";

            int[] targetArray = SearchService.Generate(size, userChoice);

            Console.WriteLine("Выберите алгоритм поиска:");
            Console.WriteLine("1. Линейный поиск");
            Console.WriteLine("2. Бинарный поиск");
            int algorithmChoice = int.Parse(Console.ReadLine());

            if (algorithmChoice == 1)
                SearchService.Set(new LinearSearcher());
            else if (algorithmChoice == 2)
                SearchService.Set(new BinarySearcher());

            Console.WriteLine("Введите элемент для поиска (от -999 до 999):");
            int toSearch = int.Parse(Console.ReadLine());

            SearchService.Do(targetArray, toSearch);
        }
    }
}
