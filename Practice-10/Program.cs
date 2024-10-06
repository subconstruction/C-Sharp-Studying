using System;
using System.Collections.Generic;

class UniversalCollection<T>
{
    List<T> items = new();
    public void Add(T item) => items.Add(item);
    public void Remove(T item) => items.Remove(item);
    public T Get(int index) => items[index];
    public void Print() => items.ForEach(i => Console.WriteLine(i));
}

class Program
{
    static void Main()
    {
        var collection = new UniversalCollection<int>();

        int elementCount;
        Console.Write("Введите количество элементов: ");
        while (!int.TryParse(Console.ReadLine(), out elementCount) || elementCount < 1)
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
        }

        for (int i = 0; i < elementCount; i++)
        {
            Console.Write($"Введите элемент {i + 1}: ");
            int element;
            while (!int.TryParse(Console.ReadLine(), out element))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
            }
            collection.Add(element);
        }

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Вывести все элементы");
            Console.WriteLine("2 - Удалить элемент");
            Console.WriteLine("3 - Получить элемент по индексу");
            Console.WriteLine("4 - Выход");
            Console.Write("Ваш выбор: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Элементы коллекции:");
                    collection.Print();
                    break;
                case "2":
                    Console.Write("Введите элемент для удаления: ");
                    int elementToRemove;
                    if (int.TryParse(Console.ReadLine(), out elementToRemove))
                    {
                        collection.Remove(elementToRemove);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод.");
                    }
                    break;
                case "3":
                    Console.Write("Введите индекс элемента: ");
                    int index;
                    if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < elementCount)
                    {
                        Console.WriteLine("Элемент: " + collection.Get(index));
                    }
                    else
                    {
                        Console.WriteLine("Некорректный индекс.");
                    }
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}
