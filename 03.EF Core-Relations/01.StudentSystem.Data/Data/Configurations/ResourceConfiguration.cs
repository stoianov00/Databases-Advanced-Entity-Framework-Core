using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.StudentSystem.Data.Data.Models;

namespace _01.StudentSystem.Data.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r => r.ResourceId);

            builder.Property(r => r.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(r => r.Url)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(r => r.ResourceType)
             .IsRequired(true);

            builder.Property(r => r.CourseId)
                .IsRequired(true);

            builder.HasOne(c => c.Course)
                .WithMany(r => r.Resources)
                .HasForeignKey(c => c.CourseId);
        }
    }
}