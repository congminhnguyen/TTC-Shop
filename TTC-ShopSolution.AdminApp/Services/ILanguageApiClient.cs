using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Common;
using TTC_ShopSolution.ViewModels.System.Languages;

namespace TTC_ShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}
