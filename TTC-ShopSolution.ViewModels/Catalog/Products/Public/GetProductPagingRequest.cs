using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Catalog.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Products.Public
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
