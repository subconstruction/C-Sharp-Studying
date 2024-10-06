using System;

class ComplexNumber
{
    public double Real { get; }
    public double Imaginary { get; }

    public ComplexNumber(double r, double i)
    {
        Real = r;
        Imaginary = i;
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) =>
        new(a.Real + b.Real, a.Imaginary + b.Imaginary);

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) =>
        new(a.Real - b.Real, a.Imaginary - b.Imaginary);

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) =>
        new(a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real);

    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denom = b.Real * b.Real + b.Imaginary * b.Imaginary;
        return new(
            (a.Real * b.Real + a.Imaginary * b.Imaginary) / denom,
            (a.Imaginary * b.Real - a.Real * b.Imaginary) / denom
        );
    }

    public override string ToString() =>
        $"{Real}{(Imaginary >= 0 ? "+" : "")}{Imaginary}i";
}

class Program
{
    static void Main()
    {
        Console.Write("Введите действительную и мнимую часть первого числа (через пробел): ");
        var parts1 = Console.ReadLine().Split();
        var a = new ComplexNumber(double.Parse(parts1[0]), double.Parse(parts1[1]));

        Console.Write("Введите действительную и мнимую часть второго числа (через пробел): ");
        var parts2 = Console.ReadLine().Split();
        var b = new ComplexNumber(double.Parse(parts2[0]), double.Parse(parts2[1]));

        Console.WriteLine($"\n{a} + {b} = {a + b}");
        Console.WriteLine($"{a} - {b} = {a - b}");
        Console.WriteLine($"{a} * {b} = {a * b}");
        Console.WriteLine($"{a} / {b} = {a / b}");
    }
}
