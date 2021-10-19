using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.Application.Dtos;

namespace TTC_ShopSolution.Application.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
