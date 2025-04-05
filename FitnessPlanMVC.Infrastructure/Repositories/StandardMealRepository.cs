using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class StandardMealRepository : IStandardMealRepository

    {
        private readonly FitnessPlanMVCDbContext _context;

        public StandardMealRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }
        public IQueryable<Meal> GetAllMeals(DateTime selectedDate)
        {
            return _context.Meals
                .Where(meal => meal.Data.Date == selectedDate.Date)
                .Include(meal => meal.MealProducts);
        }
        public int GetGrammageForProduct(int productId)
        {
            return _context.MealProducts
                .Where(mealProduct => mealProduct.ProductId == productId)
                .Select(mealProduct => mealProduct.Grammage)
                .FirstOrDefault();
        }

        public IQueryable<Meal> GetAllMealsById(int mealId)
        {
            var targetMealDate = _context.Meals
                .Where(meal => meal.Id == mealId)
                .Select(meal => meal.Data)
                .FirstOrDefault();

            return _context.Meals
                .Where(meal => meal.Data == targetMealDate)
                .Include(meal => meal.MealProducts);

        }

        public void DeleteProduct(int id)
        {
            var product = _context.MealProducts.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.MealProducts.Remove(product);
                _context.SaveChanges();
            }
        }

        public int AddProductTo(int productId, int mealId, int quantity)
        {
            var product = _context.Products.Find(productId);
            var meal = _context.Meals.Include(m => m.MealProducts).FirstOrDefault(m => m.Id == mealId);

            if (product != null)
            {
                if (meal.MealProducts == null)
                {
                    meal.MealProducts = new List<MealProduct>();
                }
                if (!meal.MealProducts.Any(p => p.ProductId == productId))
                {
                    var mealProduct = new MealProduct
                    {
                        MealId = mealId,
                        Meal = meal,
                        ProductId = productId,
                        Product = product,
                        Grammage = quantity
                    };
                    meal.MealProducts.Add(mealProduct);
                    _context.SaveChanges();
                    return meal.Id;
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }
        public void AddMeals(List<Meal> meals)
        {
            foreach (var mealVm in meals)
            {
                var meal = new Meal
                {
                    Name = mealVm.Name,
                    Data = mealVm.Data,

                };
                _context.Meals.Add(meal);
            }
            _context.SaveChanges();
        }

        public bool MealsExistForDate(DateTime selectedDate)
        {
            return _context.Meals.Any(m => m.Data.Date == selectedDate.Date);
        }
        public void AddProd(MealProduct product)
        {
            _context.MealProducts.Add(product);
            _context.SaveChanges();
        }
    }
}