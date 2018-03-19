using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }
         
        public Product(string name, decimal price, int sellerId)
        {
            this.Name = name;
            this.Price = price;
            this.SellerId = sellerId;
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }

        public User Buyer { get; set; }

        public int SellerId { get; set; }

        public User Seller { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
