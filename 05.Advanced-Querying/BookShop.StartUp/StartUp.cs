using BookShop.Data;
using BookShop.Models;
using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace BookShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new BookShopContext();
        }

        private static string Output(List<string> list)
        {
            return string.Join(Environment.NewLine, list);
        }

        private static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int enumValue = -1;

            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
                default:
                    break;
            }

            var titles = context.Books
                .Where(t => t.AgeRestriction == (AgeRestriction)enumValue)
                .Select(t => t.Title)
                .OrderBy(t => t)
                .ToList();

            return Output(titles);
        }

        private static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context.Books
                .OrderBy(t => t.BookId)
                .Where(t => t.EditionType == EditionType.Gold && t.Copies < 5_000)
                .Select(t => t.Title)
                .ToList();

            return Output(titles);
        }

        private static string GetBooksByPrice(BookShopContext context)
        {
            var titles = context.Books
                .OrderByDescending(t => t.Price)
                .Where(t => t.Price > 40)
                .Select(t => $"{t.Title} - ${t.Price}")
                .ToList();

            return Output(titles);
        }

        private static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var titles = context.Books
                .OrderBy(t => t.BookId)
                .Where(t => t.ReleaseDate.Value.Year != year)
                .Select(t => t.Title)
                .ToList();

            return Output(titles);
        }

        private static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower()
                .Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var titles = context.Books
                .OrderBy(t => t.Title)
                .Where(t => t.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(t => t.Title)
                .ToList();

            return Output(titles);
        }

        private static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            string format = "dd-MM-yyyy";
            var titles = context.Books
                .OrderByDescending(t => t.ReleaseDate)
                .Where(t => t.ReleaseDate < DateTime.ParseExact(date, format, CultureInfo.InvariantCulture))
                .Select(t => $"{t.Title} - {t.EditionType} - ${t.Price:F2}")
                .ToList();

            return Output(titles);
        }

        private static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authorNames = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToList();

            return Output(authorNames);
        }

        private static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var titles = context.Books
                .OrderBy(t => t.Title)
                .Where(t => t.Title.ToLower().Contains(input))
                .Select(t => t.Title)
                .ToList();

            return Output(titles);
        }

        private static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var titles = context.Books
                .OrderBy(t => t.BookId)
                .Where(a => a.Author.LastName.ToLower().StartsWith(input))
                .Select(t => $"{t.Title} ({t.Author.FirstName} {t.Author.LastName})")
                .ToList();

            return Output(titles);
        }

        private static string CountBooks(BookShopContext context, int lengthCheck)
        {
            var titles = context.Books
                .Where(t => t.Title.Length > lengthCheck)
                .GroupBy(c => c.Title)
                .Select(t => t)
                .ToList()
                .Count;

            return $"There are {titles} books with longer title than {lengthCheck} symbols";
        }

        private static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.Name} - {author.Copies}");
            }

            return sb.ToString();
        }

        private static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitsByCategory = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            return string.Join(Environment.NewLine, profitsByCategory.Select(p => $"{p.Name} ${p.Profit:F2}"));
        }

        private static string GetMostRecentBooks(BookShopContext context)
        {
            var categoriesWithBooks = context.Categories
                 .OrderBy(c => c.Name)
                 .Select(c => new
                 {
                     c.Name,
                     Books = c.CategoryBooks
                         .Select(cb => cb.Book)
                         .OrderByDescending(b => b.ReleaseDate)
                         .Take(3)
                 })
                 .ToList();

            return string.Join(Environment.NewLine,
                categoriesWithBooks
                .Select(c => $"--{c.Name}{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine, c.Books.Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))}"));
        }

        private static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b => b.Price += 5);

            context.SaveChanges();
        }

        private static int RemoveBooks(BookShopContext context)
        {
            var booksForDelete = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.RemoveRange(booksForDelete);
            context.SaveChanges();

            return booksForDelete.Count;
        }
    }

}