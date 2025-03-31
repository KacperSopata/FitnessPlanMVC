using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
        {
            //ViewBag.IsAdmin = User.IsInRole("Admin");
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
