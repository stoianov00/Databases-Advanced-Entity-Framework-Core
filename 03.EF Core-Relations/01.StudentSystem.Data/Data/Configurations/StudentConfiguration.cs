using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

            builder.Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsRequired(false)
                .IsUnicode(false);

            builder.Property(s => s.RegisteredOn)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.Birthday)
                .IsRequired(false);
        }
    }
}