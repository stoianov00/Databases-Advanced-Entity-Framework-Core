using System;

namespace _01.Bills_Payment_System.Data.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public DateTime ExperitionDate { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

    }
}