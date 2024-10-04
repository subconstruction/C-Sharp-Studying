using System;
using System.Collections.Generic;

class Temperature : IComparable<Temperature>
{
    private double tempk;
    private string orig;
    private string desc;

    public Temperature(string tempStr, string description)
    {
        orig = tempStr;
        desc = description;
        double tempValue;
        char scale = tempStr[tempStr.Length - 1];

        string tempNumStr = tempStr.Substring(0, tempStr.Length - 1);

        if (!double.TryParse(tempNumStr, out tempValue))
        {
            throw new ArgumentException("Некорректное значение температуры");
        }

        switch (scale)
        {
            case 'K':
                tempk = tempValue;
                break;
            case 'C':
                tempk = tempValue + 273.15;
                break;
            case 'F':
                tempk = (tempValue - 32) * 5 / 9 + 273.15;
                break;
            default:
                throw new ArgumentException("Неизвестная шкала температуры");
        }

        if (tempk < 0)
        {
            throw new ArgumentException("Температура ниже абсолютного нуля");
        }
    }

    public double C => tempk - 273.15;
    public double F => (tempk - 273.15) * 9 / 5 + 32;
    public double K => tempk;
    public string Description => desc;

    public override string ToString()
    {
        return $"{orig} — {desc}";
    }

    public int CompareTo(Temperature other)
    {
        return tempk.CompareTo(other.tempk);
    }
}

class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X:F3}, {Y:F3})";
    }
}

class DefaultCalc : IComparer<Point>
{
    public int Compare(Point p1, Point p2)
    {
        double d1 = p1.X * p1.X + p1.Y * p1.Y;
        double d2 = p2.X * p2.X + p2.Y * p2.Y;
        return d1.CompareTo(d2);
    }
}

class CompareX : IComparer<Point>
{
    public int Compare(Point p1, Point p2)
    {
        return Math.Abs(p1.Y).CompareTo(Math.Abs(p2.Y));
    }
}

class CompareY : IComparer<Point>
{
    public int Compare(Point p1, Point p2)
    {
        return Math.Abs(p1.X).CompareTo(Math.Abs(p2.X));
    }
}

class EntireCompare : IComparer<Point>
{
    public int Compare(Point p1, Point p2)
    {
        return Math.Abs(p1.X - p1.Y).CompareTo(Math.Abs(p2.X - p2.Y));
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Задание с классом Temperature:");

        string[] tempData = new string[]
        {
            "100C|Точка кипения воды",
            "0C|Точка замерзания воды",
            "273.15K|Точка замерзания воды в Кельвинах",
            "32F|Точка замерзания воды в Фаренгейтах",
            "-40C|Точка совпадения шкал Цельсия и Фаренгейта",
            "77F|Комнатная температура",
            "300K|Средняя температура"
        };

        List<Temperature> tempList = new List<Temperature>();

        foreach (var line in tempData)
        {
            string[] parts = line.Split('|');
            try
            {
                Temperature temp = new Temperature(parts[0], parts[1]);
                tempList.Add(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании Temperature: {ex.Message}");
            }
        }

        Console.WriteLine("\nИсходный список температур:");
        foreach (var temp in tempList)
        {
            Console.WriteLine(temp);
        }

        tempList.Sort();

        Console.WriteLine("\nСортировка по возрастанию температуры:");
        foreach (var temp in tempList)
        {
            Console.WriteLine($"{temp} ({temp.K:F2} K)");
        }

        tempList.Sort((t1, t2) => t2.K.CompareTo(t1.K));

        Console.WriteLine("\nСортировка по убыванию температуры:");
        foreach (var temp in tempList)
        {
            Console.WriteLine($"{temp} ({temp.K:F2} K)");
        }

        Console.WriteLine("\nЗадание с классом Point:");

        List<Point> points = GenerateRandomPoints(10);

        Console.WriteLine("\nИсходный список точек:");
        foreach (var p in points)
        {
            Console.WriteLine(p);
        }

        points.Sort(new DefaultCalc());
        
        Console.WriteLine("\nТочки, отсортированные по удалению от начала координат:");
        foreach (var p in points)
        {
            double d = Math.Sqrt(p.X * p.X + p.Y * p.Y);
            Console.WriteLine($"{p} Расстояние: {d:F3}");
        }

        points.Sort(new CompareX());

        Console.WriteLine("\nТочки, отсортированные по удалению от оси абсцисс:");
        foreach (var p in points)
        {
            double d = Math.Abs(p.Y);
            Console.WriteLine($"{p} Расстояние от оси X: {d:F3}");
        }

        points.Sort(new CompareY());

        Console.WriteLine("\nТочки, отсортированные по удалению от оси ординат:");
        foreach (var p in points)
        {
            double d = Math.Abs(p.X);
            Console.WriteLine($"{p} Расстояние от оси Y: {d:F3}");
        }

        points.Sort(new EntireCompare());

        Console.WriteLine("\nТочки, отсортированные по удалению от прямой y = x:");
        foreach (var p in points)
        {
            double d = Math.Abs(p.X - p.Y) / Math.Sqrt(2);
            Console.WriteLine($"{p} Расстояние от y = x: {d:F3}");
        }
    }

    static List<Point> GenerateRandomPoints(int n)
    {
        Random rand = new Random();
        List<Point> points = new List<Point>();
        for (int i = 0; i < n; i++)
        {
            double x = rand.NextDouble();
            double y = rand.NextDouble();
            points.Add(new Point(x, y));
        }
        return points;
    }
}
