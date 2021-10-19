using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Application.Catalog.Products.Dtos;
using TTC_ShopSolution.Application.Dtos;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int request);
        Task<List<ProductViewModel>> GetAll();
        Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
