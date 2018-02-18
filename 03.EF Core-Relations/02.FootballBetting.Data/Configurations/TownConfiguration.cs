using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.TownId);

            builder.Property(t => t.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(200);

            builder.HasOne(c => c.Country)
                .WithMany(t => t.Towns)
                .HasForeignKey(c => c.TownId);
        }
    }
}