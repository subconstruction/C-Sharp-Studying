using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Program
{
    abstract class LibraryItem
    {
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public int Year { get; protected set; }

        protected LibraryItem(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }

    class Book : LibraryItem
    {
        public string Genre { get; private set; }

        public Book(string title, string author, int year, string genre)
            : base(title, author, year)
        {
            Genre = genre;
        }
    }

    class Magazine : LibraryItem
    {
        public int IssueNumber { get; private set; }

        public Magazine(string title, string author, int year, int issueNumber)
            : base(title, author, year)
        {
            IssueNumber = issueNumber;
        }
    }

    class Borrower
    {
        public string Name { get; private set; }
        private List<LibraryItem> BorrowedItems { get; } = new();
        public decimal Fine { get; private set; }

        public Borrower(string name)
        {
            Name = name;
        }

        public void BorrowItem(LibraryItem item)
        {
            BorrowedItems.Add(item);
            Console.WriteLine($"{Name} взял: {item.Title}");
        }

        public void ReturnItem(LibraryItem item, int daysOverdue)
        {
            if (BorrowedItems.Remove(item))
            {
                Console.WriteLine($"{Name} вернул: {item.Title}");
                CalculateFine(daysOverdue);
            }
            else
            {
                Console.WriteLine($"{Name} не брал: {item.Title}");
            }
        }

        private void CalculateFine(int daysOverdue)
        {
            if (daysOverdue > 0)
            {
                decimal fineAmount = daysOverdue * 1.5m;
                Fine += fineAmount;
                Console.WriteLine($"Начислен штраф: {fineAmount} у.е.");
            }
        }

        public void ShowBorrowedItems()
        {
            Console.WriteLine($"\n{Name} взятые предметы:");
            foreach (var item in BorrowedItems)
            {
                Console.WriteLine($"- {item.Title} ({item.Author}, {item.Year})");
            }
            Console.WriteLine($"Общий штраф: {Fine} у.е.\n");
        }
    }

    class Program
    {
        static void Main()
        {
            var libraryItems = new List<LibraryItem>();

            Console.Write("Введите количество книг для добавления: ");
            if (int.TryParse(Console.ReadLine(), out int bookCount))
            {
                for (int i = 0; i < bookCount; i++)
                {
                    Console.WriteLine($"\nВведите данные для книги #{i + 1}:");
                    Console.Write("Название: ");
                    string title = Console.ReadLine();
                    Console.Write("Автор: ");
                    string author = Console.ReadLine();
                    Console.Write("Жанр: ");
                    string genre = Console.ReadLine();
                    Console.Write("Год издания: ");
                    int year = int.Parse(Console.ReadLine());

                    libraryItems.Add(new Book(title, author, year, genre));
                }
            }

            libraryItems.Add(new Magazine("Будущее 2025", "Наука", 2022, 45));
            libraryItems.Add(new Magazine("Технологии 2025", "Технологии", 2023, 12));

            var borrower = new Borrower("Сергей Иванов");

            borrower.BorrowItem(libraryItems[0]);
            borrower.BorrowItem(libraryItems[^1]);

            borrower.ReturnItem(libraryItems[0], daysOverdue: 5);

            borrower.ShowBorrowedItems();
        }
    }
}
