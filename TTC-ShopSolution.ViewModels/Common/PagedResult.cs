using System;
using System.Collections.Generic;
using System.Text;
using TTC_ShopSolution.ViewModels.Common;

namespace TTC_ShopSolution.ViewModels.Catalog.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }

    }
}
