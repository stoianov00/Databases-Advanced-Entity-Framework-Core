using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using _01.StudentSystem.Data.Data.Configurations;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
                : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\03.EF Core-Relations\connection.txt";
                var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

                builder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());
        }

    }
}
