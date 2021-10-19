using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Application.Catalog.Products.Dtos;
using TTC_ShopSolution.Application.Dtos;
using TTC_ShopSolution.Data.EF;
using TTC_ShopSolution.Data.Entities;

namespace TTC_ShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {

        private readonly TTC_ShopDBContext _context;
        public ManageProductService(TTC_ShopDBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
