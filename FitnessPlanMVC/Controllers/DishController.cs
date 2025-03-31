using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Application.ViewModels.UserMeal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class DishController : Controller
    {
        private readonly IUserMealService _UserMealService;
        private readonly IProductService _productService;

        public DishController(IUserMealService mealService, IProductService productService)
        {
            _UserMealService = mealService;
            _productService = productService;
        }

        [Authorize]
        public IActionResult Index(DateTime? selectedDate)
        {
            if (!selectedDate.HasValue || selectedDate == DateTime.MinValue)
            {
                selectedDate = DateTime.Today;
            }
            var model = _UserMealService.GetMeal(null, selectedDate.Value); // przekazujemy null jako userId
            model.Data = selectedDate.Value;
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMealToDay(DateTime mealData)
        {
            var model = new UserNewMealVm();
            model.Data = mealData;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddMealToDay(UserNewMealVm model)
        {
            var id = _UserMealService.AddMeal(model);
            DateTime mealDate = model.Data;
            return RedirectToAction("Index", new { selectedDate = mealDate });
        }

        public IActionResult DeleteMeal(int mealid, DateTime mealData)
        {
            _UserMealService.DeleteMeal(mealid);
            return RedirectToAction("Index", new { selectedDate = mealData });
        }

        [HttpGet]
        public IActionResult AddProductToMeal(int mealId, int productId)
        {
            var product = _productService.GetproductForEdit(productId);
            var model = new NewProductInUserInMealVm()
            {
                MealId = mealId,
                ProductId = productId,
                Calories = product.Calories,
                Carbohydrates = product.Carbohydrates,
                Fat = product.Fat,
                Protein = product.Protein
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToMeal(NewProductInUserInMealVm model)
        {
            bool productExists = _UserMealService.DoesProductExistInMeal(model.MealId, model.ProductId);
            if (!productExists)
            {
                _UserMealService.AddProductToMeal(model);
                var mealDate = _UserMealService.GetMealDate(model.MealId);
                return RedirectToAction("Index", new { selectedDate = mealDate });
            }
            else
            {
                TempData["ErrorMessage"] = "The product already exists in this meal.";
                return RedirectToAction("ProductList", new { mealId = model.MealId });
            }
        }

        [HttpGet]
        public IActionResult ProductList(int mealId, int pageSize = 10, int pageNo = 1, string searchString = "")
        {
            var model = _productService.GetAllProductForList(pageSize, pageNo, searchString);
            ViewBag.MealId = mealId;
            return View(model);
        }

        [HttpPost]
        public IActionResult ProductList(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = string.Empty;
            }
            var model = _productService.GetAllProductForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        public IActionResult Delete(int id, DateTime mealData)
        {
            _UserMealService.DeleteProduct(id);
            return RedirectToAction("Index", new { selectedDate = mealData });
        }

        [HttpGet]
        public IActionResult EditGrammage(int mealId)
        {
            var product = _UserMealService.GetMealProductById(mealId);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditGrammage(NewProductInUserInMealVm model)
        {
            _UserMealService.UpdateProduct(model);
            var mealDate = _UserMealService.GetMealDate(model.MealId);
            return RedirectToAction("Index", new { selectedDate = mealDate });
        }
    }
}
