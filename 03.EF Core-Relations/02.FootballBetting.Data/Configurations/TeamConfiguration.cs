using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(t => t.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);
                
            builder.Property(t => t.LogoUrl)
                .IsUnicode(false);

            builder.Property(t => t.Initials)
                .IsRequired(true)
                .HasColumnType("NCHAR(3)");

            builder.HasOne(p => p.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(p => p.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(s => s.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Town)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.TownId);
        }
    }
}