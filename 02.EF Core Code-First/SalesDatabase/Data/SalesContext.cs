using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SalesDatabase.Data.Models;

namespace SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\02.EF Core Code-First\connection.txt";
                var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Quantity)
                    .IsRequired(true);

                entity.Property(e => e.Price)
                    .IsRequired(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasDefaultValue("No Description");
            });

            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasMaxLength(80);

                entity.Property(e => e.CreditCardNumber)
                    .IsUnicode(false);
            });

            builder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(80);

            });

            builder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                    .IsRequired(true)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.Sales)
                    .HasForeignKey(e => e.ProductId);

                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Sales)
                    .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Store)
                    .WithMany(e => e.Sales)
                    .HasForeignKey(e => e.StoreId);
            });

        }

    }
}
