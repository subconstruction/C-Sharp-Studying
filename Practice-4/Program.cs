using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public interface IEntity
    {
        double Weight { get; }
        double Value { get; }
    }

    public abstract class EntityBase : IEntity
    {
        public double Weight { get; protected set; }
        public double Value { get; protected set; }

        protected EntityBase(double weight, double value)
        {
            Weight = weight;
            Value = value;
        }
    }

    public class IItem : EntityBase
    {
        public IItem(double weight, double value) : base(weight, value) { }
    }

    public interface IBackpack
    {
        double Cap { get; }
        List<IEntity> Items { get; }
        void AddItem(IEntity item);
        double TotalWeight { get; }
        double TotalValue { get; }
    }

    public class Backpack : IBackpack
    {
        public double Cap { get; private set; }
        public List<IEntity> Items { get; private set; }

        public Backpack(double cap)
        {
            Cap = cap;
            Items = new List<IEntity>();
        }

        public void AddItem(IEntity item)
        {
            if (TotalWeight + item.Weight <= Cap)
            {
                Items.Add(item);
            }
        }

        public double TotalWeight => Items.Sum(i => i.Weight);
        public double TotalValue => Items.Sum(i => i.Value);
    }

    public interface IMethod
    {
        IBackpack Exec(List<IEntity> items, double cap);
    }

    public class ASorting : IMethod
    {
        public IBackpack Exec(List<IEntity> items, double cap)
        {
            var obj = new Backpack(cap);
            var sortedItems = items.OrderByDescending(i => i.Value / i.Weight).ToList();

            foreach (var item in sortedItems)
            {
                obj.AddItem(item);
            }

            return obj;
        }
    }

    public interface IOutput
    {
        List<IEntity> GetItems();

        double GetCap();
        void DisplayResult(IBackpack obj);
    }

    public class Output : IOutput
    {
        public List<IEntity> GetItems()
        {
            Console.Write("Введите количество предметов: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                throw new ArgumentException("Неверное количество предметов.");
            }

            var items = new List<IEntity>();
            for (int i = 0; i < count; i++)
            {
                Console.Write($"Введите вес предмета {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
                {
                    throw new ArgumentException("Неверный вес предмета.");
                }

                Console.Write($"Введите стоимость предмета {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out double value) || value < 0)
                {
                    throw new ArgumentException("Неверная стоимость предмета.");
                }

                items.Add(new IItem(weight, value));
            }

            return items;
        }

        public double GetCap()
        {
            Console.Write("Введите грузоподъемность рюкзака: ");
            if (!double.TryParse(Console.ReadLine(), out double cap) || cap <= 0)
            {
                throw new ArgumentException("Неверная грузоподъемность.");
            }
            return cap;
        }

        public void DisplayResult(IBackpack obj)
        {
            Console.WriteLine($"Общая стоимость: {obj.TotalValue}");
            Console.WriteLine($"Общий вес: {obj.TotalWeight}");
            Console.WriteLine("Предметы в рюкзаке:");
            foreach (var item in obj.Items)
            {
                Console.WriteLine($"-> Вес: {item.Weight}, Стоимость: {item.Value}");
            }
        }
    }

    public class CController
    {
        private readonly IOutput _ui;
        private readonly IMethod _algorithm;

        public CController(IOutput ui, IMethod algorithm)
        {
            _ui = ui;
            _algorithm = algorithm;
        }

        public void Do()
        {
            try
            {
                var items = _ui.GetItems();
                var cap = _ui.GetCap();
                var obj = _algorithm.Exec(items, cap);
                _ui.DisplayResult(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public class EntryPoint
    {
        public static void Main()
        {
            var UI = new Output();
            var algorithm = new ASorting();
            var controller = new CController(UI, algorithm);

            controller.Do();
        }
    }
}
