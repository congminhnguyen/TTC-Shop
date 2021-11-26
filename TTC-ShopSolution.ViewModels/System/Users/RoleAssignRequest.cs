using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
