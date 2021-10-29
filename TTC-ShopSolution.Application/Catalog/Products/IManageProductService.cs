using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Catalog.Common;
using TTC_ShopSolution.ViewModels.Catalog.Products;
using TTC_ShopSolution.ViewModels.Catalog.Products.Manage;

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
        /*Task<List<ProductViewModel>> GetAll();*/

        // GetAllPaging lấy "PageIndex" + "PageSize" theo "Keyword" + "CategoryIds"
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        // sau khi thêm product, có thễ thêm/sửa/xóa riêng ảnh
        Task<int> AddImages(int productId, List<IFormFile> files);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId, string caption, bool isDefault);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

    }
}
