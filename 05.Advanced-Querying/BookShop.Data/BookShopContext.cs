using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using BookShop.Data.EntityConfiguration;
using System.IO;
using System.Text;
using System.Linq;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext() { }

        public BookShopContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = @"C:\Users\Ss\Desktop\05.Advanced-Querying\connection.txt";
                var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
        }
    }
}
