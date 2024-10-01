using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Задание 1:");
        RunPart();
        Console.WriteLine("\nЗадание 2:");
        RunTask2();
        Console.WriteLine("\nЗадание 3:");
        RunTask3();
    }

    // Задание 1
    public static void RunPart()
    {
        string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        int index = -1;

        Func<string> getNextDay = () =>
        {
            index = (index + 1) % days.Length;
            return days[index];
        };

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(getNextDay());
        }
    }

    // Задание 2
    public static Func<double, double> CreateQuadratic(double a, double b, double c)
    {
        return x => a * x * x + b * x + c;
    }

    public static void RunTask2()
    {
        var quadraticFunc = CreateQuadratic(1, -3, 2);
        double xValue = 5;
        double result = quadraticFunc(xValue);
        Console.WriteLine($"Значение квадратичного трёхчлена при x = {xValue}: {result}");
    }

    // Задание 3
    public class EventBase
    {
        public string Name;

        public EventBase(string name)
        {
            Name = name;
        }

        public event Action<string> OnEvent;

        public void GenerateEvent()
        {
            OnEvent?.Invoke(Name);
        }
    }

    public class CHandler
    {
        public void HandleEvent(string message)
        {
            Console.WriteLine($"Обработчик получил сообщение: {message}");
        }
    }

    public static void RunTask3()
    {
        EventBase obj1 = new EventBase("Объект 1");
        EventBase obj2 = new EventBase("Объект 2");

        CHandler handlerObj = new CHandler();

        obj1.OnEvent += handlerObj.HandleEvent;
        obj2.OnEvent += handlerObj.HandleEvent;

        obj1.GenerateEvent();
        obj2.GenerateEvent();
    }
}
