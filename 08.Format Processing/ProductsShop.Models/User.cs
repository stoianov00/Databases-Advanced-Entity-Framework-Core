using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class User
    {
        public User()
        {
            this.ProductsBought = new HashSet<Product>();
            this.ProductsSold = new HashSet<Product>();
        }

        public User(string firstName, string lastName, byte? age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }
         
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte? Age { get; set; }

        public ICollection<Product> ProductsBought { get; set; }

        public ICollection<Product> ProductsSold { get; set; }
    }
}
