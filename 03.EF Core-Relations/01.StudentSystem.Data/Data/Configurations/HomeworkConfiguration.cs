using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.HomeworkId);

            builder.Property(h => h.Content)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(h => h.ContentType)
                .IsRequired(true);

            builder.Property(h => h.SubmissionTime)
                .IsRequired(true);

            builder.Property(e => e.StudentId)
                .IsRequired(true);

            builder.HasOne(s => s.Student)
               .WithMany(h => h.HomeworkSubmissions)
               .HasForeignKey(s => s.StudentId);

            builder.Property(e => e.CourseId)
                .IsRequired(true);

            builder.HasOne(c => c.Course)
                .WithMany(h => h.HomeworkSubmissions)
                .HasForeignKey(c => c.CourseId);
        }
    }
}