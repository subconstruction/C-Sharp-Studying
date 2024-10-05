using System;
using System.Collections.Generic;

interface IOrder
{
    int GetOrderId();
    string GetCustomerName();
    decimal GetTotalAmount();
}

class Order : IOrder, IComparable<Order>
{
    public int OrderId { get; }
    public string CustomerName { get; }
    public decimal TotalAmount { get; }

    public Order(int id, string name, decimal amount)
    {
        OrderId = id;
        CustomerName = name;
        TotalAmount = amount;
    }

    public int GetOrderId() => OrderId;
    public string GetCustomerName() => CustomerName;
    public decimal GetTotalAmount() => TotalAmount;

    public int CompareTo(Order other) => TotalAmount.CompareTo(other.TotalAmount);

    public override string ToString() => $"Заказ №{OrderId}, Клиент: {CustomerName}, Сумма: {TotalAmount}";
}

class Program
{
    static void Main()
    {
        List<Order> orders = new();
        Console.Write("Введите количество заказов: ");
        for (int count = int.Parse(Console.ReadLine()); count > 0; count--)
        {
            Console.Write("Номер заказа: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Имя клиента: ");
            string name = Console.ReadLine();
            Console.Write("Сумма заказа: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            orders.Add(new(id, name, amount));
        }

        Console.WriteLine("\nСписок заказов:");
        orders.ForEach(o => Console.WriteLine(o));

        orders.Sort();

        Console.WriteLine("\nОтсортированный список:");
        orders.ForEach(o => Console.WriteLine(o));
    }
}
