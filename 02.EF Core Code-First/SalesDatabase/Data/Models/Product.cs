﻿using System.Collections.Generic;

namespace SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new List<Sale>();    
        }

        public int ProductId { get; set; } 

        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } 

        public ICollection<Sale> Sales { get; set; }

    }
}