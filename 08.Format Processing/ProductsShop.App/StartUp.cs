using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShop.App
{
    public class StartUp
    {
        public static void Main()
        {

        }

        // <--- XML --->
        private static string ImportUserFromXml()
        {
            string path = "Files/users.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var result = new List<User>();
            foreach (var element in elements)
            {
                string firstName = element.Attribute("firstName")?.Value;
                string lastName = element.Attribute("lastName").Value;

                byte? age = null;
                if (element.Attribute("age") != null)
                {
                    age = byte.Parse(element.Attribute("age").Value);
                }

                var user = new User(firstName, lastName, age);
                result.Add(user);
            }

            using (var context = new ProductsShopDbContext())
            {
                context.Users.AddRange(result);
                context.SaveChanges();
            }

            return $"{result.Count} users were imported from file: {path}";
        }

        private static string ImportCategoriesFromXml()
        {
            string path = "Files/categories.xml";
            var xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var result = new List<Category>();
            foreach (var element in elements)
            {
                string name = element.Element("name").Value;

                var category = new Category(name);
                result.Add(category);
            }

            using (var context = new ProductsShopDbContext())
            {
                context.Categories.AddRange(result);
                context.SaveChanges();
            }

            return $"{result.Count} categories were imported from file: {path}";
        }

        private static string ImportProductsFromXml()
        {
            string path = "Files/products.xml";
            var xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var result = new List<CategoryProduct>();
            using (var context = new ProductsShopDbContext())
            {
                var userIds = context.Users
                    .Select(u => u.UserId)
                    .ToArray();

                var categoryIds = context.Categories
                    .Select(u => u.CategoryId)
                    .ToArray();

                var random = new Random();

                foreach (var element in elements)
                {
                    string name = element.Element("name").Value;
                    decimal price = decimal.Parse(element.Element("price").Value);

                    int sellerIndex = random.Next(0, userIds.Length);
                    int sellerId = userIds[sellerIndex];

                    var product = new Product(name, price, sellerId);

                    int categoryIndex = random.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[categoryIndex];

                    var catProduct = new CategoryProduct(product, categoryId);
                    result.Add(catProduct);
                }

                context.AddRange(result);
                context.SaveChanges();
            }

            return $"{result.Count} products were imported from file: {path}";
        }

        private static void ProductsInRange()
        {
            using (var context = new ProductsShopDbContext())
            {
                var products = context.Products
                    .Where(p => (p.Price >= 1000 && p.Price <= 2000))
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                    })
                    .ToArray();

                var doc = new XElement("products");
                foreach (var product in products)
                {
                    doc.Add(new XElement("product",
                        new XAttribute("name", product.Name),
                        new XAttribute("price", product.Price),
                        new XAttribute("buyer", product.Buyer)));
                }

                doc.Save("ExportXml/ProductsInRange.xml");
            }
        }

        private static void SoldProducts()
        {
            using (var context = new ProductsShopDbContext())
            {
                var users = context.Users
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName ?? "firstName doesn't exist",
                        LastName = u.LastName,
                        SoldProducts = u.ProductsSold
                        .Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    })
                    .ToArray();

                var doc = new XElement("users");
                foreach (var user in users)
                {
                    doc.Add(new XElement("user",
                        new XAttribute("first-name", user.FirstName),
                        new XAttribute("last-name", user.LastName)),
                        new XElement("sold-products",
                        new XElement("product", new XElement("name", user.SoldProducts),
                        new XElement("price", user.SoldProducts))));
                }

                doc.Save("ExportXml/SoldProducts.xml");
            }
        }

        private static void CategoriesByProductsCountXml()
        {
            using (var context = new ProductsShopDbContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.CategoryProducts.Count)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductsCount = c.CategoryProducts.Count,
                        AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                        TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                    })
                    .ToArray();

                var doc = new XElement("categories");
                foreach (var category in categories)
                {
                    doc.Add(new XElement("category", new XAttribute("name", category.Category)),
                        new XElement("products-count", category.ProductsCount),
                        new XElement("average-price", category.AveragePrice),
                        new XElement("total-revenue", category.TotalRevenue));
                }

                doc.Save("ExportXml/CategoriesByProductsCount.xml");
            }
        }

        private static void UsersAndProductsXml()
        {
            using (var context = new ProductsShopDbContext())
            {
                var users = context.Users
                  .Where(u => u.ProductsSold.Count() > 0)
                  .OrderByDescending(u => u.ProductsSold.Count())
                  .ThenBy(u => u.LastName)
                  .Select(u => new
                  {
                      FirstName = u.FirstName ?? "firstName doesn't exist",
                      LastName = u.LastName ?? "lastName doesn't exist",
                      Age = u.Age,
                      SoldProducts = new
                      {
                          Count = u.ProductsSold.Count(),
                          Products = u.ProductsSold
                          .Select(p => new
                          {
                              Name = p.Name,
                              Price = p.Price
                          })
                      }
                  })
                  .ToArray();

                var doc = new XElement("users");
                foreach (var user in users)
                {
                    doc.Add(new XElement("user", new XAttribute("first-name", user.FirstName),
                            new XAttribute("last-name", user.LastName),
                            new XAttribute("age", user.Age)),
                            new XElement("sold-products", new XAttribute("count", user.SoldProducts.Count)),
                            new XElement("product",
                            new XAttribute("name", user.SoldProducts.Products),
                            new XAttribute("price", user.SoldProducts.Products)));
                }

                doc.Save("ExportXml/UsersAndProducts.xml");
            }
        }

        // <--- JSON --->
        private static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            var objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }

        private static string ImportUsersFromJson()
        {
            string path = "Files/users.json";
            var users = ImportJson<User>(path);

            using (var context = new ProductsShopDbContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            return $"{users.Length} users were imported from file: {path}";
        }

        private static string ImportCategoriesFromJson()
        {
            string path = "Files/categories.json";
            var categories = ImportJson<Category>(path);

            using (var context = new ProductsShopDbContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            return $"{categories.Length} categories were imported from file: {path}";
        }

        private static string ImportProductsFromJson()
        {
            string path = "Files/products.json";
            var random = new Random();

            var products = ImportJson<Product>(path);

            using (var context = new ProductsShopDbContext())
            {
                int[] userIds = context.Users
                    .Select(u => u.UserId)
                    .ToArray();

                foreach (var p in products)
                {
                    int index = random.Next(0, userIds.Length);
                    int sellerId = userIds[index];
                    int? buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        int buyerIndex = random.Next(0, userIds.Length);

                        buyerId = userIds[buyerIndex];
                    }

                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                    {
                        buyerId = null;
                    }

                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"{products.Length} products were imported from file: {path}";
        }

        private static void SetCategories()
        {
            using (var context = new ProductsShopDbContext())
            {
                var productIds = context.Products
                    .Select(p => p.ProductId)
                    .ToArray();

                var categoryIds = context.Categories
                    .Select(c => c.CategoryId)
                    .ToArray();

                var random = new Random();
                int categoryCount = categoryIds.Length;
                var categoryProductsList = new List<CategoryProduct>();
                foreach (var id in productIds)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int index = random.Next(0, categoryCount);
                        while (categoryProductsList.Any(cp => cp.ProductId == id && cp.CategoryId == categoryIds[index]))
                        {
                            index = random.Next(0, categoryCount);
                        }

                        var categoryProducts = new CategoryProduct(id, categoryIds[index]);

                        categoryProductsList.Add(categoryProducts);
                    }
                }

                context.CategoryProducts.AddRange(categoryProductsList);
                context.SaveChanges();
            }
        }

        private static void GetProductsInRange()
        {
            using (var context = new ProductsShopDbContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .ToArray();

                var result = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("ExportJson/PricesInRange.json", result);
            }
        }

        private static void SuccessfullySoldProducts()
        {
            using (var context = new ProductsShopDbContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts = u.ProductsSold
                        .Select(p => new
                        {
                            p.Name,
                            p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName
                        })
                    })
                    .ToArray();

                var result = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("ExportJson/SuccessffulySoldProducts.json", result);
            }
        }

        private static void CategoriesByProductsCount()
        {
            using (var context = new ProductsShopDbContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductsCount = c.CategoryProducts.Count,
                        AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                        TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                    })
                    .ToArray();


                var result = JsonConvert.SerializeObject(categories, Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("ExportJson/CategoriesByProductsCount.json", result);
            }
        }

        private static void UsersAndProducts()
        {
            using (var context = new ProductsShopDbContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count() > 0)
                    .OrderByDescending(u => u.ProductsSold.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Count(),
                            Products = u.ProductsSold
                            .Select(p => new
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                        }
                    })
                    .ToArray();

                var usersCountObj = new
                {
                    UsersCounts = users.Count(),
                    Users = users
                };

                var result = JsonConvert.SerializeObject(usersCountObj, Formatting.Indented);

                File.WriteAllText("ExportJson/UsersAndProducts.json", result);
            }
        }

    }
}