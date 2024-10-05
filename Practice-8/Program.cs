using System;
using System.Collections.Generic;

struct Student
{
    public int Id;
    public string Name;
    public int Age;
    public double GPA;
}

class Program
{
    static void Main()
    {
        var students = new List<Student>();
        Console.Write("Введите количество студентов: ");
        for (int n = int.Parse(Console.ReadLine()); n > 0; n--)
        {
            Console.Write("Порядковый номер: ");
            students.Add(new Student
            {
                Id = int.Parse(Console.ReadLine()),
                Name = Input("Фамилия и имя: "),
                Age = int.Parse(Input("Возраст: ")),
                GPA = double.Parse(Input("Средний балл: "))
            });
        }

        Output("Список студентов:", students);

        students.Sort((a, b) => a.Name.CompareTo(b.Name));
        Output("Список студентов, отсортированный по имени:", students);

        students.Sort((a, b) => a.Age.CompareTo(b.Age));
        Output("Список студентов, отсортированный по возрасту:", students);

        students.Sort((a, b) => a.GPA.CompareTo(b.GPA));
        Output("Список студентов, отсортированный по среднему баллу:", students);
    }

    static string Input(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    static void Output(string title, List<Student> list)
    {
        Console.WriteLine($"\n{title}");
        list.ForEach(s => Console.WriteLine($"Id: {s.Id}, Имя: {s.Name}, Возраст: {s.Age}, Средний балл: {s.GPA:F2}"));
    }
}
