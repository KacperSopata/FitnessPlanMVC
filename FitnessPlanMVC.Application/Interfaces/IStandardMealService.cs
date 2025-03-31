using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.StandardMeal;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IStandardMealService
    {
        ListStandardMealsForListVm GetMealById(int mealId);
        ListStandardMealsForListVm GetAllMealsForList(DateTime selectedData);
        void AddMealsToDay(DateTime selectedData);
        void DeleteProduct(int id);
        int AddProductMeal(NewProductStandardInMealVm productId);
    }
}
