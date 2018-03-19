using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Configuration;
using ProductsShop.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductsShop.Data
{
    public class ProductsShopDbContext : DbContext
    {
        public ProductsShopDbContext()
        {

        }

        public ProductsShopDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\08.Format Processing\connection.txt";
            var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
        }

    }
}
