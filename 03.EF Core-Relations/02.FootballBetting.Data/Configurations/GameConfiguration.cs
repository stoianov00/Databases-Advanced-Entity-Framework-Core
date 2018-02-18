using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.GameId);

            builder.HasOne(ht => ht.HomeTeam)
                .WithMany(hg => hg.HomeGames)
                .HasForeignKey(ht => ht.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(at => at.AwayTeam)
                .WithMany(ag => ag.AwayGames)
                .HasForeignKey(at => at.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}