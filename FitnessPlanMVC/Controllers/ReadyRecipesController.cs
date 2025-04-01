using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Application.ViewModels.ReadyRecipes;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationForTraining.Controllers
{
    public class ReadyRecipesController : Controller
    {
        private readonly IReadyRecipesService _readyRecipesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReadyRecipesController(IReadyRecipesService readyRecipesService, UserManager<ApplicationUser> userManager)
        {
            _readyRecipesService = readyRecipesService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var model = _readyRecipesService.GetAllReadyRecipesForList(10, 1, searchString ?? string.Empty);
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = userId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = string.Empty;
            }
            var model = _readyRecipesService.GetAllReadyRecipesForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddReadyRecipes()
        {
            return View(new NewReadyRecipesVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReadyRecipes(NewReadyRecipesVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;

            if (model.ImageContent != null && model.ImageContent.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImageContent.CopyToAsync(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }

            var id = _readyRecipesService.AddReadyRecipes(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _readyRecipesService.GetReadyRecipesDetail(id);
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recipe = _readyRecipesService.GetReadyRecipesDetail(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _readyRecipesService.DeleteReadyRecipes(id);
            return RedirectToAction("Index");
        }
    }
}