using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTC_ShopSolution.ViewModels.System.Languages;

namespace TTC_ShopSolution.AdminApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageVm> Languages { get; set; }

        public string CurrentLanguageId { get; set; }
    }
}
