using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Product;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
        {
            // Pobrane informacje o użytkowniku
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = userId;

            // Pobrać model produktów
            var model = _productService.GetAllProductForList(pageSize, pageNo, searchString);
            return View(model);
        }


        public IActionResult Details(int id)
        {
            var model = _productService.GetDetails(id);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new NewProductVm());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct(NewProductVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;
            var id = _productService.AddProduct(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = _productService.GetproductForEdit(id);
            return View(product);
        }


        [HttpPost]
        public IActionResult EditProduct(NewProductVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;
            _productService.UpdateProduct(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}