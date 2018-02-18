using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Name)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(u => u.UserName)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(u => u.Password)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(60);
        }
    }
}