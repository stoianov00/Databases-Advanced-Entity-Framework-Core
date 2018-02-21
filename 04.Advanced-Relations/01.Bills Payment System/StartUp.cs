using System;
using System.Linq;
using _01.Bills_Payment_System.Data.Data;
using _01.Bills_Payment_System.Data.Data.Models;
using _01.Bills_Payment_System.Data.Data.Models.Enums;
using System.Globalization;
using System.Text;

namespace _01.Bills_Payment_System
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new PaymentContext();

            // UserDetails(db);

        }

        private static void Seed(PaymentContext db)
        {
            var user = new User
            {
                FirstName = "George",
                LastName = "Petrov",
                Password = "qwerty",
                Email = "george@gmail.com"
            };

            var creditCards = new CreditCard[]
            {
                new CreditCard
                {
                    ExperitionDate = DateTime.ParseExact("20.05.2012", "dd.MM.yyyy", null),
                    Limit = 2000m,
                    MoneyOwed = 500m
                },

                new CreditCard
                {
                    ExperitionDate = DateTime.ParseExact("20.05.2012", "dd.MM.yyyy", null),
                    Limit = 7200m,
                    MoneyOwed = 1500m
                }
            };

            var bankAccount = new BankAccount
            {
                Balance = 5000,
                BankName = "Bank",
                SwiftCode = "SWWS"
            };

            var paymentMehods = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    User = user,
                    CreditCard = creditCards[0],
                    Type = PaymentType.CreditCard
                },

                new PaymentMethod()
                {
                    User = user,
                    CreditCard = creditCards[1],
                    Type = PaymentType.CreditCard
                },

                new PaymentMethod()
                {
                    User = user,
                    BankAccount = bankAccount,
                    Type = PaymentType.CreditCard
                }
            };

            db.Users.Add(user);
            db.CreditCards.AddRange(creditCards);
            db.BankAccounts.Add(bankAccount);
            db.PaymentMethods.AddRange(paymentMehods);

            db.SaveChanges();
        }

        private static void UserDetails(PaymentContext db)
        {
            int userId = int.Parse(Console.ReadLine());

            var user = db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    Name = $"{u.FirstName} {u.LastName}",

                    BankAccounts = db.BankAccounts.Select(ba => ba).ToList(),
                    CreditCards = db.CreditCards.Select(cc => cc).ToList()
                })
                .FirstOrDefault();

            foreach (var item in user.BankAccounts)
            {
                var builder = new StringBuilder();

                builder.AppendLine("Bank Accounts:")
                .AppendLine($"-- ID: {item.BankAccountId}")
                .AppendLine($"--- Balance: {item.Balance:F2}")
                .AppendLine($"--- Bank: {item.BankName}")
                .Append($"--- SWIFT: {item.SwiftCode}");

                Console.WriteLine(OutputFormat(builder));
            }

            foreach (var item in user.CreditCards)
            {
                var builder = new StringBuilder();

                builder.AppendLine("Credit Cards:")
                .AppendLine($"-- ID: {item.CreditCardId}")
                .AppendLine($"--- Limit: {item.Limit:F2}")
                .AppendLine($"--- Money Owed: {item.MoneyOwed:F2}")
                .AppendLine($"--- Limit Left: {item.LimitLeft:F2}")
                .Append($"--- Expiration Date: {item.ExperitionDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");

                Console.WriteLine(OutputFormat(builder));
            }
        }

        private static string OutputFormat(StringBuilder builder)
        {
            return builder.ToString();
        }

    }
}