using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .IsUnicode(true);

            builder.Property(c => c.StartDate);

            builder.Property(c => c.EndDate);

            builder.Property(c => c.Price);
        }
    }
}