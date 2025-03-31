using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.ReadyRecipes
{
    public class ListReadyRecipesVm
    {
        public List<ReadyRecipesForListVm> ReadyRecipes { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
