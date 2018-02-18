using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data.Data.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.HasOne(sc => sc.Student)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);
            
            builder.HasOne(c => c.Course)
                .WithMany(sc => sc.StudentsEnrolled)
                .HasForeignKey(c => c.CourseId);
        }
    }
}