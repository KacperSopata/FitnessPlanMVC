﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.StandardMeal
{
    public class ListStandardMealsForListVm
    {
        public List<StandardMealForListVm> Meals { get; set; }
        public List<StandardMealDetailVm> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
