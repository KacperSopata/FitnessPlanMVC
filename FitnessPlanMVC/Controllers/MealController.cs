using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.StandardMeal;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class MealController : Controller
    {
        private readonly IStandardMealService _mealService;
        private readonly IProductService _productService;

        public MealController(IStandardMealService mealService, IProductService productService)
        {
            _mealService = mealService;
            _productService = productService;
        }


        public IActionResult Index(DateTime selectedDate)
        {
            if (selectedDate == DateTime.MinValue)
            {
                selectedDate = DateTime.Today;
            }

            var model = _mealService.GetAllMealsForList(selectedDate);
            return View(model);
        }


        public IActionResult AddMealsToDay(DateTime selectedDate)
        {
            if (selectedDate == DateTime.MinValue)
            {
                selectedDate = DateTime.Today;
            }

            _mealService.AddMealsToDay(selectedDate);
            var model = _mealService.GetAllMealsForList(selectedDate);
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult AddProductToMeal(int productId, int mealId)
        {
            var model = new NewProductStandardInMealVm()
            {
                MealId = mealId,
                ProductId = productId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToMeal(NewProductStandardInMealVm model)
        {
            var id = _mealService.AddProductMeal(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult ListOfProduct(int mealId)
        {
            TempData["MealId"] = mealId;
            var model = _productService.GetAllProductForList(10, 1, "");
            return View(model);
        }
        [HttpPost]
        public IActionResult ListOfProduct(int pageSize, int? pageNo, string searchString, int mealId)
        {
            TempData["MealId"] = mealId;

            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }

            if (searchString is null)
            {
                searchString = String.Empty;
            }

            var model = _productService.GetAllProductForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }
        public IActionResult DeleteProduct(int id)
        {
            _mealService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}