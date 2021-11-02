using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Catalog.Common;
using TTC_ShopSolution.ViewModels.Catalog.ProductImages;
using TTC_ShopSolution.ViewModels.Catalog.Products;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int request);
        Task<ProductViewModel> GetById(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        /*Task<List<ProductViewModel>> GetAll();*/

        // GetAllPaging lấy "PageIndex" + "PageSize" theo "Keyword" + "CategoryIds"
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        // sau khi thêm product, có thễ thêm/sửa/xóa riêng ảnh
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<List<ProductImageViewModel>> GetListImage(int productId);

    }
}
