using System.Collections.Generic;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Catalog.Common;
using TTC_ShopSolution.ViewModels.Catalog.Products;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}
