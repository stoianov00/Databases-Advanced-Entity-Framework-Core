using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Models;

namespace Workshop.Data.Configurations
{
    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name)
                .IsUnique(true);

            builder.Property(t => t.Name)
                .IsRequired(true)
                .HasMaxLength(25);

            builder.Property(t => t.Description)
                .HasMaxLength(32);

            builder.Property(t => t.Acronym)
                .HasMaxLength(3)
                .HasColumnType("CHAR(3)")
                .IsRequired(true);

            builder.HasOne(e => e.Creator)
                .WithMany(u => u.CreatedTeams)
                .HasForeignKey(e => e.CreatorId);
        }
    }
}
