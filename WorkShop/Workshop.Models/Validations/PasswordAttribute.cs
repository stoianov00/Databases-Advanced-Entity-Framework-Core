using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workshop.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class PasswordAttribute : ValidationAttribute
    {
        private int minLength;
        private int maxLength;

        internal PasswordAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public bool ContainsUppercase { get; set; }

        public bool ContainsDigit { get; set; }

        public override bool IsValid(object value)
        {
            string password = value.ToString();

            if (password.Length < this.minLength || password.Length > this.maxLength)
            {
                return false;
            }

            if (this.ContainsDigit && !password.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            if (this.ContainsUppercase && !password.Any(c => char.IsUpper(c)))
            {
                return false;
            }

            return true;
        }
    }
}
