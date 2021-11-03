using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Common
{
    public class PagingRequestBase : RequestBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
