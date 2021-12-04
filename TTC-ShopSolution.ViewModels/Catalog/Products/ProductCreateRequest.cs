using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TTC_ShopSolution.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public decimal? OriginalPrice { set; get; }
        public int? Stock { set; get; }

        public string? Description { set; get; }
        public string? Details { set; get; }
        public string? SeoAlias { set; get; }
        public IFormFile ThumbnailImage { set; get; }

        // bỏ SeoDescription
        // bỏ SeoTitle

    }
}
