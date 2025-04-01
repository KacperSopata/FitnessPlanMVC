using Microsoft.AspNetCore.Mvc;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Post;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FitnessPlanMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;

        }
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var model = _postService.GetAllPostForList(10, 1, searchString ?? string.Empty);
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
            var model = _postService.GetAllPostForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            return View(new NewPostVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostVm model)
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

            var id = _postService.AddPost(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _postService.GetPostDetail(id);
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recipe = _postService.GetPostDetail(id);
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
            _postService.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}