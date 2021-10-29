using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Catalog.Common;
using TTC_ShopSolution.ViewModels.Catalog.Products;
using TTC_ShopSolution.ViewModels.Catalog.Products.Public;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
