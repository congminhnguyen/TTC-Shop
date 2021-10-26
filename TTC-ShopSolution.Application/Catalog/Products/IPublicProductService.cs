using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.Application.Catalog.Products.Dtos.Public;
using TTC_ShopSolution.Application.Dtos;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos
{
    public interface IPublicProductService
    {
        public PagedResult<ProductViewModel> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
