using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.Application.Dtos;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos
{
    public interface IPublicProductService
    {
        public PageViewModel<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize);
    }
}
