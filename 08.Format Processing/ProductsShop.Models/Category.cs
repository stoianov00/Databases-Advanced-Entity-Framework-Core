using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }

        public Category(string name)
        {
            this.Name = name;
        }

        public int CategoryId { get; set; }
         
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
