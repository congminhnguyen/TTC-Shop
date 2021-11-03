﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_ShopSolution.ViewModels.Catalog.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
        public int TotalRecords { get; set; }
    }
}
