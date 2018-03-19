using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsShop.Models;

namespace ProductsShop.Data.Configuration
{
    internal class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(cp => new { cp.ProductId, cp.CategoryId });

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.ProductId);

            builder.HasOne(cp => cp.Category)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}
