using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class UsernameAttribute : ValidationAttribute
    {
        private int minLength;
        private int maxLength;

        internal UsernameAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            string username = value.ToString();

            if (username.Length < this.minLength || username.Length > this.maxLength)
            {
                return false;
            }

            return true;
        }
    }
}
