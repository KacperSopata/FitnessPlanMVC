using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IStandardMealRepository
    {
        IQueryable<Meal> GetAllMeals(DateTime selectedData);
        int AddProductTo(int productId, int mealId, int quantity);
        int GetGrammageForProduct(int productId);
        void AddMeals(List<Meal> meals);
        bool MealsExistForDate(DateTime selectedDate);
        IQueryable<Meal> GetAllMealsById(int mealId);
        void DeleteProduct(int id);
        void AddProd(MealProduct product);
    }
}
