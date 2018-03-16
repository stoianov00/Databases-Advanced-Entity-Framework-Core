using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .HasMaxLength(60)
                .IsRequired(true);

            builder.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsRequired(true);

            builder.Property(e => e.Salary)
                .HasMaxLength(60)
                .IsRequired(true);

            builder.Property(e => e.Address)
                .HasMaxLength(250);
        }
    }
}