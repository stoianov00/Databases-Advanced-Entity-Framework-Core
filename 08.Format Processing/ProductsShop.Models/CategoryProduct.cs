namespace ProductsShop.Models
{
    public class CategoryProduct
    {
        public CategoryProduct()
        {

        }

        public CategoryProduct(int productId, int categoryId)
        {
            this.ProductId = productId;
            this.CategoryId = categoryId;
        }

        public CategoryProduct(Product product, int categoryId)
        {
            this.Product = product;
            this.CategoryId = categoryId;
        }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
