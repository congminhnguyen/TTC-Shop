using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Catalog.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Products.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
