using eShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Data.EF;
using TTC_ShopSolution.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TTC_ShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using TTC_ShopSolution.Application.Common;
using TTC_ShopSolution.ViewModels.Catalog.ProductImages;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public class ProductService : IProductService
    {

        private readonly TTC_ShopDBContext _context;
        private readonly IStorageService _storageService;
        public ProductService(TTC_ShopDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        /*----------------------------------- phần Add -----------------------------------*/
        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };
            // Save image
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
             await _context.SaveChangesAsync();
            return productImage.Id;
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
                Name = request.Name,
                Price = (decimal) request.Price,
                OriginalPrice = (decimal)request.OriginalPrice,
                Stock = (int)request.Stock,
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
             await _context.SaveChangesAsync();
            return product.Id;
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
        public Task<List<ProductVm>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductVm>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        // không làm productTranSlations
                        //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.Id
                        //where pt.LanguageId == request.LanguageId //ko làm language
                        select new { p };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            //if (request.CategoryIds != null && request.CategoryIds.Count > 0)
            //{
            //    query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            //}

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
               .Select(x => new ProductVm()
               {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = (DateTime)x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = (decimal)x.p.OriginalPrice,
                    Price = (decimal)x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    Stock = (int)x.p.Stock,
                    ViewCount = (int)x.p.ViewCount
                }).ToListAsync();


            //4. Select and projection
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize, 
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            //var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId
            //&& x.LanguageId == languageId);

            var productViewModel = new ProductVm()
            {
                Id = product.Id,
                DateCreated = (DateTime)product.DateCreated,
                Description = product.Description,
                Details = product.Details,
                Name = product.Name,
                OriginalPrice = (decimal)product.OriginalPrice,
                Price = (decimal)product.Price,
                SeoAlias = product.SeoAlias,
                Stock = (int)product.Stock,
                ViewCount = (int)product.ViewCount
            };
            return productViewModel;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new EShopException($"Cannot find a Image with id: {imageId}");
            }
            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId
            };
            return viewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).Select(i => new ProductImageViewModel()
            {
                Caption = i.Caption,
                DateCreated = i.DateCreated,
                FileSize = i.FileSize,
                Id = i.Id,
                ImagePath = i.ImagePath,
                IsDefault = i.IsDefault,
                ProductId = i.ProductId
            }).ToListAsync();
        }

        /*----------------------------------- phần Remove -----------------------------------*/
        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EShopException($"Cannot find a product with id: {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        /*----------------------------------- phần Update -----------------------------------*/
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            // không làm productTranSlations
            if (product == null) 
                throw new EShopException($"Cannot find a product with id: {request.Id}");

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

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopException($"Cannot find an image with id {imageId}");
            };
            // Save image
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
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

        public async Task<PagedResult<ProductVm>> GetAllByCategoryId(GetPublicProductPagingRequest request)
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
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
      .Select(x => new ProductVm()
      {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = (DateTime)x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = (decimal)x.p.OriginalPrice,
                    Price = (decimal)x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    Stock = (int)x.p.Stock,
                    ViewCount = (int)x.p.ViewCount
                }).ToListAsync();


            //4. Select and projection
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize, 
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
    }

 
}
