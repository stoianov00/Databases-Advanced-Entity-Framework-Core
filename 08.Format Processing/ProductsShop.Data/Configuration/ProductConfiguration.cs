using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsShop.Models;

namespace ProductsShop.Data.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name)
                .IsUnicode(true)
                .HasMaxLength(200);

            builder.HasOne(p => p.Buyer)
                .WithMany(u => u.ProductsBought)
                .HasForeignKey(p => p.BuyerId);

            builder.HasOne(p => p.Seller)
                .WithMany(u => u.ProductsSold)
                .HasForeignKey(p => p.SellerId);
        }
    }
}
