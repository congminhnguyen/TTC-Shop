using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
