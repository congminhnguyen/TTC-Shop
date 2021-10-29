using eShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Data.EF;
using TTC_ShopSolution.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TTC_ShopSolution.ViewModels.Catalog.Products.Manage;
using TTC_ShopSolution.ViewModels.Catalog.Products;
using TTC_ShopSolution.ViewModels.Catalog.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using TTC_ShopSolution.Application.Common;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {

        private readonly TTC_ShopDBContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(TTC_ShopDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        /*----------------------------------- phần Add -----------------------------------*/
        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        /*----------------------------------- phần Create -----------------------------------*/
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                SeoAlias = request.SeoAlias
                // không làm product tranSlations
            };

            // Save image
            if(request.ThumbnailImage !=null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        /*----------------------------------- phần Delete -----------------------------------*/
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");

            // 1. xóa ảnh trước
            // tìm ảnh trùng với productId tại vị trí i 
            var images = _context.ProductImages.Where(i => i.ProductId == productId); 
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            // 2. xóa tất cả sau
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        /*----------------------------------- phần Get -----------------------------------*/
        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        // không làm productTranSlations
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };

            //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
            //            join c in _context.Categories on pic.CategoryId equals c.Id
            //            select new { p, pic };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();


            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                //PageSize = request.PageSize, //cái này video sau
                //PageIndex = request.PageIndex, //cái này video sau
                Items = data
            };
            return pagedResult;
        }

        public Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            throw new NotImplementedException();
        }

        /*----------------------------------- phần Remove -----------------------------------*/
        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        /*----------------------------------- phần Update -----------------------------------*/
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            // không làm productTranSlations
            if (product == null) throw new EShopException($"Cannot find a product with id: {request.Id}");

        // không làm productTranSlations
        // không làm productTranSlations
        // không làm productTranSlations
        // không làm productTranSlations

        // Save image
        if (request.ThumbnailImage != null)
        {
            var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
            if (thumbnailImage != null)
            {
                thumbnailImage.FileSize = request.ThumbnailImage.Length;
                thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                _context.ProductImages.Update(thumbnailImage);
            }
        }
            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            // không làm productTranSlations
            if (product == null) throw new EShopException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0; // lớn là true. nhỏ hoặc bằng thì false
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            // không làm productTranSlations
            if (product == null) throw new EShopException($"Cannot find a product with id: {productId}");
            product.Price = addedQuantity;
            return await _context.SaveChangesAsync() > 0; // lớn là true. nhỏ hoặc bằng thì false
        }

        /*----------------------------------- phần mở rộng -----------------------------------*/
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }

 
}
