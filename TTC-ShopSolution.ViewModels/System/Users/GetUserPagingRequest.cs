using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Catalog.Common;

namespace TTC_ShopSolution.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
