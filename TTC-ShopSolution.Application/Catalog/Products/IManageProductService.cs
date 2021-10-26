using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Application.Catalog.Products.Dtos;
using TTC_ShopSolution.Application.Catalog.Products.Dtos.Manage;
using TTC_ShopSolution.Application.Dtos;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
    }
}
