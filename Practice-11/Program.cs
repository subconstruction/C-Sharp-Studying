using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите задание (1-10) или 0 для выхода:");
            Console.WriteLine("1 - Проверка строки на наличие только букв и цифр");
            Console.WriteLine("2 - Извлечение дат в формате 'dd.mm.yyyy'");
            Console.WriteLine("3 - Проверка строки на соответствие формату электронной почты");
            Console.WriteLine("4 - Замена чисел в строке на слово 'NUMBER'");
            Console.WriteLine("5 - Извлечение ссылок (URL) из текста");
            Console.WriteLine("6 - Проверка строки на наличие хотя бы одной заглавной буквы");
            Console.WriteLine("7 - Извлечение номеров телефонов в формате '+XXX-XXX-XXXXXXX'");
            Console.WriteLine("8 - Проверка строки на наличие слова, начинающегося с заглавной буквы");
            Console.WriteLine("9 - Замена слов 'apple' и 'orange' на 'fruit'");
            Console.WriteLine("10 - Проверка строки на соответствие формату IP-адреса");
            Console.WriteLine("0 - Выход");
            Console.Write("-> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
                    break;
                case "3":
                    Task3();
                    break;
                case "4":
                    Task4();
                    break;
                case "5":
                    Task5();
                    break;
                case "6":
                    Task6();
                    break;
                case "7":
                    Task7();
                    break;
                case "8":
                    Task8();
                    break;
                case "9":
                    Task9();
                    break;
                case "10":
                    Task10();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void Task1()
    {
        Console.WriteLine("\nЗадание 1: Проверка строки на наличие только букв и цифр");
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();
        string pattern = @"^[\p{L}\d]+$";
        bool result = Regex.IsMatch(input, pattern);
        Console.WriteLine($"Строка содержит только буквы и цифры: {result}");
    }

    static void Task2()
    {
        Console.WriteLine("\nЗадание 2: Извлечение всех дат в формате 'dd.mm.yyyy'");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        string pattern = @"\b\d{2}\.\d{2}\.\d{4}\b";
        MatchCollection dates = Regex.Matches(text, pattern);
        Console.WriteLine("Найденные даты:");
        foreach (Match date in dates)
        {
            Console.WriteLine(date.Value);
        }
    }

    static void Task3()
    {
        Console.WriteLine("\nЗадание 3: Проверка строки на соответствие формату электронной почты");
        Console.Write("Введите электронную почту: ");
        string email = Console.ReadLine();
        string pattern = @"^[\w\.-]+@[\w\.-]+\.[a-zA-Z]{2,7}$";
        bool result = Regex.IsMatch(email, pattern);
        Console.WriteLine($"Строка является электронной почтой: {result}");
    }

    static void Task4()
    {
        Console.WriteLine("\nЗадание 4: Замена всех чисел в строке на слово 'NUMBER'");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        string pattern = @"\d+";
        string result = Regex.Replace(text, pattern, "NUMBER");
        Console.WriteLine("Результат замены:");
        Console.WriteLine(result);
    }

    static void Task5()
    {
        Console.WriteLine("\nЗадание 5: Извлечение всех ссылок (URL) из текста");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        string pattern = @"https?://[^\s]+";
        MatchCollection links = Regex.Matches(text, pattern);
        Console.WriteLine("Найденные ссылки:");
        foreach (Match link in links)
        {
            Console.WriteLine(link.Value);
        }
    }

    static void Task6()
    {
        Console.WriteLine("\nЗадание 6: Проверка строки на наличие хотя бы одной заглавной буквы");
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();
        string pattern = @".*\p{Lu}.*";
        bool result = Regex.IsMatch(input, pattern);
        Console.WriteLine($"Строка содержит хотя бы одну заглавную букву: {result}");
    }

    static void Task7()
    {
        Console.WriteLine("\nЗадание 7: Извлечение номеров телефонов в формате '+XXX-XXX-XXXXXXX'");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        string pattern = @"\+\d{3}-\d{3}-\d{7}";
        MatchCollection phones = Regex.Matches(text, pattern);
        Console.WriteLine("Найденные номера телефонов:");
        foreach (Match phone in phones)
        {
            Console.WriteLine(phone.Value);
        }
    }

    static void Task8()
    {
        Console.WriteLine("\nЗадание 8: Проверка строки на наличие слова, начинающегося с заглавной буквы");
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();
        string pattern = @"\b\p{Lu}\p{L}*\b";
        bool result = Regex.IsMatch(input, pattern);
        Console.WriteLine($"Строка содержит слово, начинающееся с заглавной буквы: {result}");
    }

    static void Task9()
    {
        Console.WriteLine("\nЗадание 9: Замена слов 'apple' и 'orange' на 'fruit'");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        string pattern = @"\b(apple|orange)\b";
        string result = Regex.Replace(text, pattern, "fruit", RegexOptions.IgnoreCase);
        Console.WriteLine("Результат замены:");
        Console.WriteLine(result);
    }

    static void Task10()
    {
        Console.WriteLine("\nЗадание 10: Проверка строки на соответствие формату IP-адреса");
        Console.Write("Введите IP-адрес: ");
        string ip = Console.ReadLine();
        string pattern = @"^((25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){3}" +
                         @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";
        bool result = Regex.IsMatch(ip, pattern);
        Console.WriteLine($"Строка является IP-адресом: {result}");
    }
}
