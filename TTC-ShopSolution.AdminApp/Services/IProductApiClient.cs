using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Catalog.Products;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetPagings(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);
    }
}
