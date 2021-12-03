using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTC_ShopSolution.Data.EF;
using TTC_ShopSolution.ViewModels.Common;
using TTC_ShopSolution.ViewModels.System.Languages;

namespace TTC_ShopSolution.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _config;
        private readonly TTC_ShopDBContext _context;

        public LanguageService(TTC_ShopDBContext context,
            IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public Task<ApiResult<List<LanguageVm>>> GetAll()
        {
            throw new NotImplementedException();
        }

        //public async Task<ApiResult<List<LanguageVm>>> GetAll()
        //{
        //    //không có table Languages
        //    var languages = await _context.Languages.Select(x => new LanguageVm() 
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }).ToListAsync();
        //    return new ApiSuccessResult<List<LanguageVm>>(languages);
        //}
    }
}
