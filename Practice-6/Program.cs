using System;

class Shape
{
    public double Measure(double radius)
    {
        return Math.PI * radius * radius;
    }

    public double Measure(double width, double height)
    {
        return width * height;
    }

    public double Measure(double baseLength, double height, bool isRightTriangle)
    {
        if (isRightTriangle)
        {
            return 0.5 * baseLength * height;
        }
        else
        {
            double sideA = baseLength;
            double sideB = Math.Sqrt(Math.Pow(baseLength / 2, 2) + Math.Pow(height, 2));
            double sideC = sideB;
            double s = (sideA + sideB + sideC) / 2;
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
        }
    }
}

class Program
{
    static void Main()
    {
        Shape shape = new Shape();

        double areaCircle = shape.Measure(5.0);
        Console.WriteLine($"Площадь круга: {areaCircle:F2}");

        double areaRectangle = shape.Measure(4.0, 6.0);
        Console.WriteLine($"Площадь прямоугольника: {areaRectangle:F2}");

        double areaRightTriangle = shape.Measure(5.0, 7.0, true);
        Console.WriteLine($"Площадь прямоугольного треугольника: {areaRightTriangle:F2}");

        double areaTriangle = shape.Measure(5.0, 7.0, false);
        Console.WriteLine($"Площадь треугольника: {areaTriangle:F2}");
    }
}
