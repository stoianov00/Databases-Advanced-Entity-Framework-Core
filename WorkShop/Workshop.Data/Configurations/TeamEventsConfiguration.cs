using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Models;

namespace Workshop.Data.Configurations
{
    internal class TeamEventsConfiguration : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasKey(tc => new { tc.TeamId, tc.EventId });

            builder.HasOne(et => et.Team)
                .WithMany(t => t.EventParticipated)
                .HasForeignKey(et => et.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(et => et.Event)
                .WithMany(e => e.ParticipatingEventTeams)
                .HasForeignKey(et => et.EventId);

        }
    }
}
