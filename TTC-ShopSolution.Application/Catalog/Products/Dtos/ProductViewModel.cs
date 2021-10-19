﻿using System;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public string SeoAlias { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }

    }
}