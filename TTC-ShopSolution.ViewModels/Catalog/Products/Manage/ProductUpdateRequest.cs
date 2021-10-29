using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_ShopSolution.ViewModels.Catalog.Products.Manage
{
    public class ProductUpdateRequest
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }

        public string SeoAlias { set; get; }
        public IFormFile ThumbnailImage { set; get; }

        // bỏ SeoDescription
        // bỏ SeoTitle
    }
}
