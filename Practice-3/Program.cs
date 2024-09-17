using System;

namespace Program
{
    public abstract class TaskExecutor
    {
        public abstract void Unleash();
    }

    public class Fibonacci : TaskExecutor
    {
        private readonly int _numberOfElements;

        public Fibonacci(int numberOfElements)
        {
            _numberOfElements = numberOfElements;
        }

        public override void Unleash()
        {
            Console.WriteLine("Запуск генерации последовательности Фибоначчи:");
            for (int i = 0; i < _numberOfElements; i++)
            {
                Console.WriteLine($"[{i}] -> {Do(i)}");
            }
        }

        private int Do(int index)
        {
            if (index <= 1)
                return index;
            else
                return Do(index - 1) + Do(index - 2);
        }
    }

    public class TaskAnalyzer : TaskExecutor
    {
        private readonly string _phrase;

        public TaskAnalyzer(string phrase)
        {
            _phrase = phrase;
        }

        public override void Unleash()
        {
            if (Analizer(_phrase))
            {
                Console.WriteLine($"Фраза '{_phrase}' является палиндромом.");
            }
            else
            {
                Console.WriteLine($"Фраза '{_phrase}' не является палиндромом.");
            }
        }

        private bool Analizer(string text)
        {
            if (text.Length <= 1)
                return true;
            else if (text[0] != text[text.Length - 1])
                return false;
            else
                return Analizer(text.Substring(1, text.Length - 2));
        }
    }

    public class Builder
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Выберите метод:");

            Console.WriteLine("1 - Генерация последовательности Фибоначчи");
            Console.WriteLine("2 - Анализ симметрии палиндрома");

            Console.Write("-> ");

            string selection = Console.ReadLine();

            TaskExecutor executor = null;

            switch (selection)
            {
                case "1":
                    Console.Write("Введите количество элементов для генерации Фибоначчи: ");
                    if (int.TryParse(Console.ReadLine(), out int elements))
                    {
                        executor = new Fibonacci(elements);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Введите корректное целое число.");
                        return;
                    }
                    break;
                case "2":
                    Console.Write("Введите фразу для анализа на палиндром: ");
                    string phrase = Console.ReadLine();
                    executor = new TaskAnalyzer(phrase);
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            executor?.Unleash();
        }
    }
}
