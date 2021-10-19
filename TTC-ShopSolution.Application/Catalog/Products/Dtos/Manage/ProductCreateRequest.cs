using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos.Manage
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }

        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoAlias { set; get; }

    }
}
