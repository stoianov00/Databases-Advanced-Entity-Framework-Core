using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _01.Bills_Payment_System.Data.Data.Models;

namespace _01.Bills_Payment_System.Data.Configurations
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(c => c.CreditCardId);

            builder.Ignore(c => c.LimitLeft);

            builder.Ignore(c => c.PaymentMethodId);
        }
    }
}