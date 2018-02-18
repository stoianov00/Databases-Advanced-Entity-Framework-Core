using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(100);
            
            builder.Property(p => p.IsInjured)
                .HasDefaultValue(false);

            builder.HasOne(t => t.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(t => t.TeamId);

            builder.HasOne(t => t.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(t => t.PositionId);

        }
    }
}