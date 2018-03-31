using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using Workshop.Data.Configurations;
using Workshop.Models;
 
namespace Workshop.Data
{
    public class TeamBuilderDbContext : DbContext
    {
        public TeamBuilderDbContext()
        {

        }

        public TeamBuilderDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Event> Events { get; set; }
         
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<TeamEvent> EventTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\WorkShop\connection.txt";
            var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new InvitationConfiguration());
            modelBuilder.ApplyConfiguration(new UserTeamsConfiguration());
            modelBuilder.ApplyConfiguration(new TeamEventsConfiguration());
        }
    }
}
