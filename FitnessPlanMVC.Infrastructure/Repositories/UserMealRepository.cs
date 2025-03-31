using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using FitnessPlanMVC.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitnessPlanMVC.Infrastructure.Repositories;

public class UserMealRepository : IUserMealRepository
{
    private readonly FitnessPlanMVCDbContext _context;

    public UserMealRepository(FitnessPlanMVCDbContext context)
    {
        _context = context;
    }
    public List<Meal> GetMeal(DateTime selectedDate, string userId)
    {
        return _context.Meals
            .Where(e => e.Data == selectedDate)
            .ToList();
    }
    public List<Product> GetProduct(int mealId, string userId)
    {
        return _context.MealProducts
            .Where(e => e.MealsId == mealId)
            .Include(e => e.Product)
            .Select(e => e.Product)
            .ToList();
    }
    public int GetGrammageForProduct(int productId, int mealId)
    {
        return _context.MealProducts
            .Where(mealProduct => mealProduct.ProductsId == productId && mealProduct.MealsId == mealId)
            .Select(mealProduct => mealProduct.Grammage)
            .FirstOrDefault();
    }

    public int AddMeal(Meal meal)
    {
        _context.Meals.Add(meal);
        _context.SaveChanges();
        return meal.Id;
    }
    public void DeleteMeal(int mealid)
    {
        var mealProducts = _context.MealProducts.Where(mp => mp.MealsId == mealid).ToList();
        if (mealProducts.Any())
        {
            _context.MealProducts.RemoveRange(mealProducts);
        }
        var meal = _context.Meals.Find(mealid);
        if (meal != null)
        {
            _context.Meals.Remove(meal);
        }
        _context.SaveChanges();
    }
    public int AddProduct(MealProduct prod)
    {
        prod.Meal = _context.Meals.Include(m => m.MealProducts).FirstOrDefault(m => m.Id == prod.MealsId);
        prod.Product = _context.Products.Find(prod.ProductsId);
        _context.MealProducts.Add(prod);
        _context.SaveChanges();
        return prod.Id;
    }

    public DateTime GetMealData(int modelMealId)
    {
        var meal = _context.Meals
            .Where(meal => meal.Id == modelMealId)
            .Select(meal => meal.Data)
            .FirstOrDefault();

        return meal;
    }

    public void UpdateProduct(MealProduct mealproduct)
    {
        _context.Update(mealproduct);
        _context.Entry(mealproduct).Property("Grammage").IsModified = true;
        _context.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = _context.MealProducts.FirstOrDefault(d => d.ProductsId == id);
        if (product != null)
        {
            _context.MealProducts.Remove(product);
            _context.SaveChanges();
        }
    }

    public MealProduct GetMealProductById(int id)
    {
        var mealproduct = _context.MealProducts.FirstOrDefault(i => i.MealsId == id);
        return mealproduct;
    }

    public bool DoesProductExistInMeal(int modelMealId, int modelProductId)
    {
        return _context.Set<MealProduct>().Any(mp => mp.MealsId == modelMealId && mp.ProductsId == modelProductId);
    }
}