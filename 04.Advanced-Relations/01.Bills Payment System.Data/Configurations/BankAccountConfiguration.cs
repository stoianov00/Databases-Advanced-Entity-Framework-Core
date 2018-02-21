using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.Bills_Payment_System.Data.Data.Models;

namespace _01.Bills_Payment_System.Data.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(b => b.BankAccountId);

            builder.Property(b => b.BankName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(b => b.SwiftCode)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.Ignore(b => b.PaymentMethodId);
        }
    }
}