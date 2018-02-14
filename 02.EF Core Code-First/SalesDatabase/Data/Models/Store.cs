using System.Collections.Generic;

namespace SalesDatabase.Data.Models
{
    public class Store
    {
        public Store()
        {
            this.Sales = new List<Sale>();
        }

        public int StoreId { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}