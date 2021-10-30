using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Catalog.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
