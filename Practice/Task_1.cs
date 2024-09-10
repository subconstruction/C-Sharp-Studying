using System;
using System.Diagnostics;

abstract class SortingSample
{
    public abstract string Name { get; }
    public abstract int Sort(int[] arr);

    public (long Time, int Swaps, int Size) Measure(int[] arr)
    {
        var sw = Stopwatch.StartNew();
        int swaps = Sort(arr);
        sw.Stop();
        return (sw.ElapsedMilliseconds, swaps, arr.Length);
    }
}

class BubbleSort : SortingSample
{
    public override string Name => "Сортировка Пурызьком";

    public override int Sort(int[] arr)
    {
        int swaps = 0;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    swaps++;
                }
            }
        }
        return swaps;
    }
}

class InsertionSort : SortingSample
{
    public override string Name => "Сортировка Вставками";

    public override int Sort(int[] arr)
    {
        int swaps = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i], j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
                swaps++;
            }
            arr[j + 1] = key;
        }
        return swaps;
    }
}

class SelectionSort : SortingSample
{
    public override string Name => "Сортировка Выбором";

    public override int Sort(int[] arr)
    {
        int swaps = 0;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIdx])
                {
                    minIdx = j;
                }
            }
            if (minIdx != i)
            {
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                swaps++;
            }
        }
        return swaps;
    }
}

class QuickSort : SortingSample
{
    public override string Name => "Быстрая Сортировка";

    public override int Sort(int[] arr)
    {
        return QuickSortImpl(arr, 0, arr.Length - 1);
    }

    private int QuickSortImpl(int[] arr, int low, int high)
    {
        if (low >= high)
        {
            return 0;
        }

        int swaps = 0;
        int pivot = Partition(arr, low, high, ref swaps);
        swaps += QuickSortImpl(arr, low, pivot - 1);
        swaps += QuickSortImpl(arr, pivot + 1, high);
        return swaps;
    }

    private int Partition(int[] arr, int low, int high, ref int swaps)
    {
        int pivot = arr[high], i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
                swaps++;
            }
        }
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        swaps++;
        return i + 1;
    }
}

class MergeSort : SortingSample
{
    public override string Name => "Сортировка Слиянием";

    public override int Sort(int[] arr)
    {
        return MergeSortImpl(arr, new int[arr.Length], 0, arr.Length - 1);
    }

    private int MergeSortImpl(int[] arr, int[] tmp, int left, int right)
    {
        if (left >= right)
        {
            return 0;
        }

        int swaps = 0, mid = (left + right) / 2;
        swaps += MergeSortImpl(arr, tmp, left, mid);
        swaps += MergeSortImpl(arr, tmp, mid + 1, right);
        swaps += Merge(arr, tmp, left, mid, right);
        return swaps;
    }

    private int Merge(int[] arr, int[] tmp, int left, int mid, int right)
    {
        int swaps = 0, i = left, j = mid + 1, k = left;

        while (i <= mid && j <= right)
        {
            if (arr[i] <= arr[j])
            {
                tmp[k++] = arr[i++];
            }
            else
            {
                tmp[k++] = arr[j++];
                swaps += (mid - i + 1);
            }
        }
        while (i <= mid)
        {
            tmp[k++] = arr[i++];
        }
        while (j <= right)
        {
            tmp[k++] = arr[j++];
        }

        Array.Copy(tmp, left, arr, left, right - left + 1);

        return swaps;
    }
}

class ShakerSort : SortingSample
{
    public override string Name => "Шейкерная Сортировка";

    public override int Sort(int[] arr)
    {
        int swaps = 0, left = 0, right = arr.Length - 1;
        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                    swaps++;
                }
            }
            right--;
            for (int i = right; i > left; i--)
            {
                if (arr[i - 1] > arr[i])
                {
                    (arr[i - 1], arr[i]) = (arr[i], arr[i - 1]);
                    swaps++;
                }
            }
            left++;
        }
        return swaps;
    }
}

class DataSorting
{
    public static void Do(SortingSample[] methods, int[] sizes, int runs)
    {
        BeatifyHead();
        foreach (int size in sizes)
        {
            Console.WriteLine($"\nПрогон Размера: {size}\n\n");

            foreach (var method in methods)
            {
                long bestTime = long.MaxValue;
                for (int i = 0; i < runs; i++)
                {
                    int[] arr = GenerateRandomArray(size);
                    var (time, _, _) = method.Measure(arr);
                    if (time < bestTime)
                    {
                        bestTime = time;
                    }
                }
                Print(method.Name, bestTime, size);
            }
        }
        BeatifyBody();
    }

    static int[] GenerateRandomArray(int n)
    {
        var rnd = new Random();
        var arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = rnd.Next(1000);
        }
        return arr;
    }

    static void BeatifyHead()
    {
        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"|{"Метод Сортировки",-200}|{"Рантайм",-20}|{"Кол-во Элементов",-10}|");
        Console.WriteLine(new string('-', 60));
    }

    static void Print(string name, long time, int Counter)
    {
        Console.WriteLine($"|{name,-15}|{time,-20}|{Counter,-10}|");
    }

    static void BeatifyBody()
    {
        Console.WriteLine(new string('-', 60));
    }
}

class Program
{
    static void Main()
    {
        SortingSample[] methods = {
            new BubbleSort(), new InsertionSort(), new SelectionSort(),
            new QuickSort(), new MergeSort(), new ShakerSort()
        };

        int[] test = { 1000, 10000, 100000 };
        int runs = 5;

        DataSorting.Do(methods, test, runs);
    }
}
