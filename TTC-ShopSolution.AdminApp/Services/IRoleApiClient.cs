using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.Common;
using TTC_ShopSolution.ViewModels.System.Roles;

namespace TTC_ShopSolution.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
