using eShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Application.Catalog.Products.Dtos;
using TTC_ShopSolution.Application.Catalog.Products.Dtos.Manage;
using TTC_ShopSolution.Application.Dtos;
using TTC_ShopSolution.Data.EF;
using TTC_ShopSolution.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {

        private readonly TTC_ShopDBContext _context;
        public ManageProductService(TTC_ShopDBContext context)
        {
            _context = context;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

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
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        //public async Task<List<ProductViewModel>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<PagedResult<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                     
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

        public async Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            throw new NotImplementedException();
        }
    }

 
}
