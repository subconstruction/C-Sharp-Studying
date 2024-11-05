using System;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatternsDemo
{
    #region Singleton Pattern

    public sealed class ConfigurationManager
    {
        private static ConfigurationManager _instance = null;
        private static readonly object _lock = new object();

        public string ApplicationTitle { get; private set; }
        public string ApplicationVersion { get; private set; }
        public Dictionary<string, string> Settings { get; private set; }

        private ConfigurationManager()
        {
            ApplicationTitle = "Design Patterns Demo";
            ApplicationVersion = "1.0.0";
            Settings = new Dictionary<string, string>
            {
                { "Theme", "Light" },
                { "Language", "English" }
            };
        }

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void UpdateSetting(string key, string value)
        {
            if (Settings.ContainsKey(key))
            {
                Settings[key] = value;
                Console.WriteLine($"Setting '{key}' updated to '{value}'.");
            }
            else
            {
                throw new ArgumentException($"Setting key '{key}' does not exist.");
            }
        }

        public string GetSetting(string key)
        {
            if (Settings.ContainsKey(key))
            {
                return Settings[key];
            }
            else
            {
                throw new ArgumentException($"Setting key '{key}' does not exist.");
            }
        }
    }

    #endregion

    #region Factory Method Pattern - Documents

    public abstract class AbstractDocument
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public AbstractDocument()
        {
            CreatedAt = DateTime.Now;
        }

        public abstract void Render();
    }

    public class TextDocument : AbstractDocument
    {
        public string Content { get; set; }

        public override void Render()
        {
            Console.WriteLine($"--- Text Document: {Title} ---");
            Console.WriteLine($"Created At: {CreatedAt}");
            Console.WriteLine($"Content: {Content}");
        }
    }

    public class GraphicDocument : AbstractDocument
    {
        public byte[] ImageData { get; set; }

        public override void Render()
        {
            Console.WriteLine($"--- Graphic Document: {Title} ---");
            Console.WriteLine($"Created At: {CreatedAt}");
            Console.WriteLine($"Image Data: {ImageData.Length} bytes");
        }
    }

    public class SpreadsheetDocument : AbstractDocument
    {
        public Dictionary<string, double> Cells { get; set; }

        public SpreadsheetDocument()
        {
            Cells = new Dictionary<string, double>();
        }

        public override void Render()
        {
            Console.WriteLine($"--- Spreadsheet Document: {Title} ---");
            Console.WriteLine($"Created At: {CreatedAt}");
            foreach (var cell in Cells)
            {
                Console.WriteLine($"Cell {cell.Key}: {cell.Value}");
            }
        }
    }

    public abstract class DocumentFactory
    {
        public abstract AbstractDocument CreateDocument(string title);
    }

    public class TextDocumentFactory : DocumentFactory
    {
        public override AbstractDocument CreateDocument(string title)
        {
            return new TextDocument
            {
                Title = title,
                Content = "This is a sample text document."
            };
        }
    }

    public class GraphicDocumentFactory : DocumentFactory
    {
        public override AbstractDocument CreateDocument(string title)
        {
            return new GraphicDocument
            {
                Title = title,
                ImageData = new byte[] { 255, 0, 0, 255 }
            };
        }
    }

    public class SpreadsheetDocumentFactory : DocumentFactory
    {
        public override AbstractDocument CreateDocument(string title)
        {
            var spreadsheet = new SpreadsheetDocument
            {
                Title = title
            };
            spreadsheet.Cells.Add("A1", 123.45);
            spreadsheet.Cells.Add("B2", 67.89);
            return spreadsheet;
        }
    }

    #endregion

    #region Factory Method Pattern - Vehicles

    public abstract class AbstractVehicle
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public AbstractVehicle(string model, string manufacturer)
        {
            Model = model;
            Manufacturer = manufacturer;
        }

        public abstract void Drive();
    }

    public class Car : AbstractVehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(string model, string manufacturer, int doors)
            : base(model, manufacturer)
        {
            NumberOfDoors = doors;
        }

        public override void Drive()
        {
            Console.WriteLine($"Driving a {Manufacturer} {Model} with {NumberOfDoors} doors.");
        }
    }

    public class Bicycle : AbstractVehicle
    {
        public bool HasGears { get; set; }

        public Bicycle(string model, string manufacturer, bool hasGears)
            : base(model, manufacturer)
        {
            HasGears = hasGears;
        }

        public override void Drive()
        {
            string gearsInfo = HasGears ? "with gears" : "without gears";
            Console.WriteLine($"Riding a {Manufacturer} {Model} {gearsInfo}.");
        }
    }

    public abstract class VehicleFactory
    {
        public abstract AbstractVehicle CreateVehicle(string model, string manufacturer);
    }

    public class CarFactory : VehicleFactory
    {
        private int _doors;

        public CarFactory(int doors)
        {
            _doors = doors;
        }

        public override AbstractVehicle CreateVehicle(string model, string manufacturer)
        {
            return new Car(model, manufacturer, _doors);
        }
    }

    public class BicycleFactory : VehicleFactory
    {
        private bool _hasGears;

        public BicycleFactory(bool hasGears)
        {
            _hasGears = hasGears;
        }

        public override AbstractVehicle CreateVehicle(string model, string manufacturer)
        {
            return new Bicycle(model, manufacturer, _hasGears);
        }
    }

    #endregion

    #region Builder Pattern - Pizza

    public class Pizza
    {
        public string Size { get; set; }
        public string Dough { get; set; }
        public string Sauce { get; set; }
        public List<string> Toppings { get; set; }

        public Pizza()
        {
            Toppings = new List<string>();
        }

        public override string ToString()
        {
            string toppings = Toppings.Count > 0 ? string.Join(", ", Toppings) : "No Toppings";
            return $"Pizza - Size: {Size}, Dough: {Dough}, Sauce: {Sauce}, Toppings: {toppings}";
        }
    }

    public abstract class PizzaBuilder
    {
        protected Pizza _pizza;

        public PizzaBuilder()
        {
            _pizza = new Pizza();
        }

        public abstract void SetSize();
        public abstract void SetDough();
        public abstract void SetSauce();
        public abstract void AddToppings();

        public Pizza GetPizza()
        {
            return _pizza;
        }
    }

    public class MargheritaPizzaBuilder : PizzaBuilder
    {
        public override void SetSize()
        {
            _pizza.Size = "Medium";
            Console.WriteLine("Set Size: Medium");
        }

        public override void SetDough()
        {
            _pizza.Dough = "Thin Crust";
            Console.WriteLine("Set Dough: Thin Crust");
        }

        public override void SetSauce()
        {
            _pizza.Sauce = "Tomato Sauce";
            Console.WriteLine("Set Sauce: Tomato Sauce");
        }

        public override void AddToppings()
        {
            _pizza.Toppings.Add("Mozzarella");
            _pizza.Toppings.Add("Basil");
            Console.WriteLine("Added Toppings: Mozzarella, Basil");
        }
    }

    public class PepperoniPizzaBuilder : PizzaBuilder
    {
        public override void SetSize()
        {
            _pizza.Size = "Large";
            Console.WriteLine("Set Size: Large");
        }

        public override void SetDough()
        {
            _pizza.Dough = "Thick Crust";
            Console.WriteLine("Set Dough: Thick Crust");
        }

        public override void SetSauce()
        {
            _pizza.Sauce = "Garlic Sauce";
            Console.WriteLine("Set Sauce: Garlic Sauce");
        }

        public override void AddToppings()
        {
            _pizza.Toppings.Add("Pepperoni");
            _pizza.Toppings.Add("Cheddar Cheese");
            _pizza.Toppings.Add("Olives");
            Console.WriteLine("Added Toppings: Pepperoni, Cheddar Cheese, Olives");
        }
    }

    public class CustomPizzaBuilder : PizzaBuilder
    {
        private List<string> _customToppings;

        public CustomPizzaBuilder(List<string> customToppings)
        {
            _customToppings = customToppings;
        }

        public override void SetSize()
        {
            Console.Write("Enter Pizza Size (Small, Medium, Large): ");
            _pizza.Size = Console.ReadLine();
            Console.WriteLine($"Set Size: {_pizza.Size}");
        }

        public override void SetDough()
        {
            Console.Write("Enter Dough Type (Thin Crust, Thick Crust, Stuffed Crust): ");
            _pizza.Dough = Console.ReadLine();
            Console.WriteLine($"Set Dough: {_pizza.Dough}");
        }

        public override void SetSauce()
        {
            Console.Write("Enter Sauce Type (Tomato Sauce, Pesto, Alfredo Sauce): ");
            _pizza.Sauce = Console.ReadLine();
            Console.WriteLine($"Set Sauce: {_pizza.Sauce}");
        }

        public override void AddToppings()
        {
            Console.WriteLine("Enter Toppings separated by commas (e.g., Pepperoni, Mushrooms, Onions): ");
            string input = Console.ReadLine();
            var toppings = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var topping in toppings)
            {
                _pizza.Toppings.Add(topping.Trim());
            }
            Console.WriteLine($"Added Toppings: {string.Join(", ", _pizza.Toppings)}");
        }
    }

    public class PizzaDirector
    {
        private PizzaBuilder _builder;

        public void SetBuilder(PizzaBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructPizza()
        {
            if (_builder == null)
            {
                throw new InvalidOperationException("Builder not set.");
            }

            _builder.SetSize();
            _builder.SetDough();
            _builder.SetSauce();
            _builder.AddToppings();
        }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Пользовательнский ввод ===");
                Console.WriteLine("1. Singleton Pattern");
                Console.WriteLine("2. Factory Method Pattern - Documents");
                Console.WriteLine("3. Factory Method Pattern - Vehicles");
                Console.WriteLine("4. Builder Pattern - Pizza");
                Console.WriteLine("5. Exit");
                Console.Write("Выберите опцию (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        SingletonDemo();
                        break;
                    case "2":
                        FactoryMethodDocumentsDemo();
                        break;
                    case "3":
                        FactoryMethodVehiclesDemo();
                        break;
                    case "4":
                        BuilderPizzaDemo();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Выход из программы. До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        static void SingletonDemo()
        {
            ConfigurationManager config = ConfigurationManager.Instance;

            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- Singleton Pattern Demo ---");
                Console.WriteLine("1. Просмотреть настройки");
                Console.WriteLine("2. Обновить настройку");
                Console.WriteLine("3. Назад в главное меню");
                Console.Write("Выберите опцию (1-3): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Application Title: {config.ApplicationTitle}");
                        Console.WriteLine($"Application Version: {config.ApplicationVersion}");
                        foreach (var setting in config.Settings)
                        {
                            Console.WriteLine($"{setting.Key}: {setting.Value}");
                        }
                        break;
                    case "2":
                        Console.Write("Введите ключ настройки, которую хотите обновить: ");
                        string key = Console.ReadLine();
                        Console.Write("Введите новое значение: ");
                        string value = Console.ReadLine();
                        try
                        {
                            config.UpdateSetting(key, value);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        static void FactoryMethodDocumentsDemo()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- Factory Method Pattern - Documents Demo ---");
                Console.WriteLine("1. Создать текстовый документ");
                Console.WriteLine("2. Создать графический документ");
                Console.WriteLine("3. Создать таблицу");
                Console.WriteLine("4. Назад в главное меню");
                Console.Write("Выберите опцию (1-4): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                DocumentFactory factory = null;
                AbstractDocument document = null;

                switch (choice)
                {
                    case "1":
                        factory = new TextDocumentFactory();
                        Console.Write("Введите название текстового документа: ");
                        string textTitle = Console.ReadLine();
                        document = factory.CreateDocument(textTitle);
                        break;
                    case "2":
                        factory = new GraphicDocumentFactory();
                        Console.Write("Введите название графического документа: ");
                        string graphicTitle = Console.ReadLine();
                        document = factory.CreateDocument(graphicTitle);
                        break;
                    case "3":
                        factory = new SpreadsheetDocumentFactory();
                        Console.Write("Введите название таблицы: ");
                        string sheetTitle = Console.ReadLine();
                        document = factory.CreateDocument(sheetTitle);
                        break;
                    case "4":
                        back = true;
                        continue;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        continue;
                }

                document?.Render();
            }
        }

        static void FactoryMethodVehiclesDemo()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- Factory Method Pattern - Vehicles Demo ---");
                Console.WriteLine("1. Создать автомобиль");
                Console.WriteLine("2. Создать велосипед");
                Console.WriteLine("3. Назад в главное меню");
                Console.Write("Выберите опцию (1-3): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                VehicleFactory factory = null;
                AbstractVehicle vehicle = null;

                switch (choice)
                {
                    case "1":
                        factory = CarFactorySetup();
                        if (factory == null) break;

                        Console.Write("Введите модель автомобиля: ");
                        string carModel = Console.ReadLine();
                        Console.Write("Введите производителя автомобиля: ");
                        string carManufacturer = Console.ReadLine();

                        vehicle = factory.CreateVehicle(carModel, carManufacturer);
                        break;
                    case "2":
                        factory = BicycleFactorySetup();
                        if (factory == null) break;

                        Console.Write("Введите модель велосипеда: ");
                        string bikeModel = Console.ReadLine();
                        Console.Write("Введите производителя велосипеда: ");
                        string bikeManufacturer = Console.ReadLine();

                        vehicle = factory.CreateVehicle(bikeModel, bikeManufacturer);
                        break;
                    case "3":
                        back = true;
                        continue;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        continue;
                }

                vehicle?.Drive();
            }
        }

        static VehicleFactory CarFactorySetup()
        {
            while (true)
            {
                Console.Write("Введите количество дверей (2, 3, 4, 5): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int doors) && doors >= 2 && doors <= 5)
                {
                    return new CarFactory(doors);
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 2 до 5.");
                }
            }
        }

        static VehicleFactory BicycleFactorySetup()
        {
            while (true)
            {
                Console.Write("Есть ли у велосипеда передачи? (yes/no): ");
                string input = Console.ReadLine().ToLower();
                if (input == "yes" || input == "y")
                {
                    return new BicycleFactory(true);
                }
                else if (input == "no" || input == "n")
                {
                    return new BicycleFactory(false);
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите 'yes' или 'no'.");
                }
            }
        }

        static void BuilderPizzaDemo()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- Builder Pattern - Pizza Demo ---");
                Console.WriteLine("1. Создать Маргариту");
                Console.WriteLine("2. Создать Пепперони");
                Console.WriteLine("3. Создать пользовательскую пиццу");
                Console.WriteLine("4. Назад в главное меню");
                Console.Write("Выберите опцию (1-4): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                PizzaDirector director = new PizzaDirector();
                PizzaBuilder builder = null;

                switch (choice)
                {
                    case "1":
                        builder = new MargheritaPizzaBuilder();
                        break;
                    case "2":
                        builder = new PepperoniPizzaBuilder();
                        break;
                    case "3":
                        builder = new CustomPizzaBuilder(new List<string>());
                        break;
                    case "4":
                        back = true;
                        continue;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        continue;
                }

                director.SetBuilder(builder);
                director.ConstructPizza();
                Pizza pizza = builder.GetPizza();
                Console.WriteLine("\nГотовая пицца:");
                Console.WriteLine(pizza);
            }
        }
    }
}
