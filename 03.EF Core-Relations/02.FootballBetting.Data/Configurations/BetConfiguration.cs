using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _02.FootballBetting.Data.Models;

namespace _02.FootballBetting.Data.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.BetId);

            builder.HasOne(g => g.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(g => g.GameId);

            builder.HasOne(u => u.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(u => u.UserId);
        }
    }
}