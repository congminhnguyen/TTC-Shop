using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_ShopSolution.Data.Entities
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { get; set; }

        public string Description { set; get; }
        public string Details { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public string SeoAlias { set; get; }
        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }
    }
}
