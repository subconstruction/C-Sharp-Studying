// Пара 1
public class PersonalComputer
{
    public string Model { get; private set; }
    public double ProcessorFrequencyGHz { get; private set; }
    public int RamSizeGB { get; private set; }
    public int HardDiskSizeGB { get; private set; }

    public PersonalComputer(string model, double processorFrequencyGHz, int ramSizeGB, int hardDiskSizeGB)
    {
        Model = model;
        ProcessorFrequencyGHz = processorFrequencyGHz;
        RamSizeGB = ramSizeGB;
        HardDiskSizeGB = hardDiskSizeGB;
    }

    public string InfoStr()
    {
        return $"Модель: {Model}, Процессор: {ProcessorFrequencyGHz} GHz, Оперативка: {RamSizeGB} GB, Жесткий Диск: {HardDiskSizeGB} GB";
    }
}

// Пара 2
public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GroupNumber { get; set; }
    public double AvgRate { get; set; }

    public Student()
    {
        FirstName = "N/A";
        LastName = "N/a";
        GroupNumber = "0";
        AvgRate = 0.0;
    }

    public Student(string firstName, string lastName, string groupNumber, double avgRate)
    {
        FirstName = firstName;
        LastName = lastName;
        GroupNumber = groupNumber;
        AvgRate = avgRate;
    }

    public void OutputInfo()
    {
        Console.WriteLine($"Имя: {FirstName}, Фамилия: {LastName}, Номер группы: {GroupNumber}, Средний балл: {AvgRate}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Student[] students = new Student[]
        {
            new Student("Иван", "Иванов", "101", 4.5),
            new Student("Мария", "Петрова", "102", 4.7),
            new Student("Антон", "Сидоров", "103", 4.2),
            new Student()
        };

        foreach (Student student in students)
        {
            student.OutputInfo();
        }

        Student topStudent = students[0];
        foreach (Student student in students)
        {
            if (student.AvgRate > topStudent.AvgRate)
            {
                topStudent = student;
            }
        }

        Console.WriteLine("\nСтудент с наивысшим средним баллом:");
        topStudent.OutputInfo();
    }
}
