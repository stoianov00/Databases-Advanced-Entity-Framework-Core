namespace PhotoShare.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Configuration;
    using System.Text;
    using System.IO;
    using System.Linq;
     
    public class PhotoShareContext : DbContext
    {
        public PhotoShareContext() { }

        public PhotoShareContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<AlbumRole> AlbumRoles { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<AlbumTag> AlbumTags { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfig());
            modelBuilder.ApplyConfiguration(new AlbumRoleConfig());
            modelBuilder.ApplyConfiguration(new AlbumTagConfig());
            modelBuilder.ApplyConfiguration(new FriendshipConfig());
            modelBuilder.ApplyConfiguration(new PictureConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new TownConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\06.Good-Practice\connection.txt";
            var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

            optionsBuilder.UseSqlServer(connection);
        }
    }
}